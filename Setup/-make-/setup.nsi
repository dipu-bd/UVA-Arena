;NSIS Modern User Interface
;
;UVA Arena Setup Program
;Written by Sudipto Chandra

;--------------------------------

!ifdef NOCOMPRESS
SetCompress off
!endif

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh" 

;--------------------------------
;General

  ;Name and file
  Name "UVA Arena"
  OutFile "..\Windows\UVA_Arena_1.4_x86_win.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\Sand Soft\UVA Arena"

  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\UVA Arena" "Installation Path"

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin
 
;--------------------------------
;Variables

  Var StartMenuFolder

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING
  
;--------------------------------
;Pages

  ;Start Menu Folder Page Configuration
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT HKCU
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\UVA Arena" 
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
  
  ;installer pages
  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "UVA Arena\LICENSE"
  !insertmacro MUI_PAGE_COMPONENTS  
  !insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder
  !insertmacro MUI_PAGE_DIRECTORY 
  !insertmacro MUI_PAGE_INSTFILES
  ;!insertmacro MUI_PAGE_FINISH
    
  ;uninstaller pages
  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  ;!insertmacro MUI_UNPAGE_FINISH
   
;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "UVA Arena" MainSect

  ; Set output path to the installation directory.
  SetOutPath "$INSTDIR"

  ; Put my file there
  File "UVA Arena\CategoryEditor.exe"
  File "UVA Arena\CategoryEditor.exe.config"
  File "UVA Arena\CategoryEditor.exe.manifest"
  File "UVA Arena\FastColoredTextBox.DLL"
  File "UVA Arena\HtmlAgilityPack.dll"
  File "UVA Arena\LICENSE"
  File "UVA Arena\main.ico"
  File "UVA Arena\Newtonsoft.Json.dll"
  File "UVA Arena\ObjectListView.DLL"
  File "UVA Arena\PDFLibNet.dll"
  File "UVA Arena\UVA Arena.exe"
  File "UVA Arena\UVA Arena.exe.config"
  File "UVA Arena\UVA Arena.exe.manifest"
  File "UVA Arena\ZedGraph.dll"
  
  ; Write the installation path into the registry
  WriteRegStr HKCU "Software\UVA Arena" "Installation Path" $INSTDIR

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  
  ;Create Desktop shortcuts
  CreateShortCut "$DESKTOP\UVA Arena.lnk" "$INSTDIR\UVA Arena.exe" "" "$INSTDIR\UVA Arena.exe" 0

  ;Create Start Menu shortcuts
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\CategoryEditor.lnk" "$INSTDIR\CategoryEditor.exe"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\LICENSE.lnk" "$INSTDIR\LICENSE"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\UVA Arena.lnk" "$INSTDIR\UVA Arena.exe"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_END
      
SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_MainSect ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${MainSect} $(DESC_MainSect)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"
  
  ;Remove registry keys
  DeleteRegKey HKCU "Software\UVA Arena\Category Index"
  
  ; Remove files and uninstaller
  Delete "$INSTDIR\CategoryEditor.exe"
  Delete "$INSTDIR\CategoryEditor.exe.config"
  Delete "$INSTDIR\CategoryEditor.exe.manifest"
  Delete "$INSTDIR\FastColoredTextBox.DLL"
  Delete "$INSTDIR\HtmlAgilityPack.dll"
  Delete "$INSTDIR\LICENSE"
  Delete "$INSTDIR\main.ico"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\ObjectListView.DLL"
  Delete "$INSTDIR\PDFLibNet.dll"
  Delete "$INSTDIR\UVA Arena.exe"
  Delete "$INSTDIR\UVA Arena.exe.config"
  Delete "$INSTDIR\UVA Arena.exe.manifest"
  Delete "$INSTDIR\ZedGraph.dll"  
  Delete "$INSTDIR\Uninstall.exe"
  RMDir "$INSTDIR"
      
  ; Remove shortcuts, if any  
  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
    
  Delete "$SMPROGRAMS\$StartMenuFolder\CategoryEditor.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\LICENSE.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\UVA Arena.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk"
  RMDir "$SMPROGRAMS\$StartMenuFolder"

SectionEnd
