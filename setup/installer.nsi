;NSIS Modern User Interface
;Written by Sudipto Chandra

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh"

;--------------------------------
;Define variables

  !define Company "Sand Soft"
  !define AppName "UVA Arena"
  !define AppVersion "1.9.1"
  !define AppExeName "UVA Arena.exe"
  !define SetupIcon "setup.ico"
  !define LicenseFile "..\LICENSE"
  !define SetupFileName "UVA_Arena_${AppVersion}.exe"
  !define DistFiles "publish\Application Files\UVA Arena_1_9_1_82"

  !define UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${AppName}"

;--------------------------------
;General

  Unicode True

  ;Name and file
  Name "${AppName}"
  Icon "${SetupIcon}"
  OutFile "${SetupFileName}"

  ;Default installation folder
  InstallDir "$PROGRAMFILES64\${AppName}"

  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\${Company}\${AppName}" ""

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING
  !define MUI_ICON "${SetupIcon}"

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "${LicenseFile}"
  !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "${AppName}" AppFiles

  SetOutPath "$INSTDIR"

  ;Add files to installer
  File /r "${DistFiles}\*"

  ;Create prorams and features entry
  WriteRegStr SHCTX "${UNINST_KEY}" "Publisher" "Sand Soft"
  WriteRegStr SHCTX "${UNINST_KEY}" "DisplayName" "${AppName}"
  WriteRegStr SHCTX "${UNINST_KEY}" "DisplayVersion" "${AppVersion}"
  WriteRegStr SHCTX "${UNINST_KEY}" "DisplayIcon" "$INSTDIR\main.ico"
  WriteRegStr SHCTX "${UNINST_KEY}" "InstallLocation" "$INSTDIR"
  WriteRegStr SHCTX "${UNINST_KEY}" "HelpLink" "https://github.com/dipu-bd/UVA-Arena"
  WriteRegStr SHCTX "${UNINST_KEY}" "UninstallString" "$\"$INSTDIR\Uninstall.exe$\""

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd

Section "Start Menu Shortcut" StartMenu
  CreateDirectory '$SMPROGRAMS\${Company}\${AppName}'
  CreateShortCut '$SMPROGRAMS\${Company}\${AppName}\${AppName}.lnk' '$INSTDIR\${AppExeName}' "" '$INSTDIR\${AppExeName}' 0
  CreateShortCut '$SMPROGRAMS\${Company}\${AppName}\Uninstall ${AppName}.lnk' '$INSTDIR\Uninstall.exe' "" '$INSTDIR\Uninstall.exe' 0
SectionEnd

Section "Desktop Shortcut" Desktop
  SetShellVarContext current
  CreateShortCut "$DESKTOP\${AppName}.lnk" "$INSTDIR\${AppExeName}"
SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString AppDescription ${LANG_ENGLISH} "Install ${AppName}. This will overwrite files of the previous version."
  LangString StartMenuDescription ${LANG_ENGLISH} "Create Start Menu shortcuts."
  LangString DesktopDescription ${LANG_ENGLISH} "Create a Desktop shortcut."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${AppFiles} $(AppDescription)
    !insertmacro MUI_DESCRIPTION_TEXT ${StartMenu} $(StartMenuDescription)
    !insertmacro MUI_DESCRIPTION_TEXT ${Desktop} $(DesktopDescription)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  RMDir "$INSTDIR"
  RMDir "$SMPROGRAMS\${Company}\${AppName}"
  Delete "$DESKTOP\${AppName}.lnk"

  DeleteRegKey SHCTX "${UNINST_KEY}"

SectionEnd
