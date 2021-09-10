# UVA-Arena   
   
[![UVA Arena Icon](Images/Main.png)](Images/Main.png)    

**UVA Arena** (c) 2014, is a windows based, non-commercial, open source utility application to make problem solving easier for the programmers. It is based on popular [UVA Online Judge](http://uva.onlinejudge.org/) and uses APIs from [uHunt](http://uhunt.felix-halim.net/).        
 
### Download  
To view a list of new features [click here](https://github.com/dipu-bd/UVA-Arena/wiki/What's-New)        

#### Version 1.9
[![Download v1.9](Images/quick-download-media-file-image.png)](https://sourceforge.net/projects/uvaarena/files/UVA_Arena_1.9/UVA_Arena_1.9.0.exe)     

<details>
  <summary>View older versions!</summary>
	
#### Version 1.8 [Update 2]
1. [Download v1.8.2 (Windows Only)](https://sourceforge.net/projects/uvaarena/files/UVA%20Arena%201.8/UVA_Arena_1.8.2.exe)
2. **Mirror**: [GitHub release](https://github.com/dipu-bd/UVA-Arena/releases/download/v1.8.2/UVA_Arena_1.8.2.exe)

#### Version 1.7
1. [Download Setup File (Windows Only)](https://github.com/dipu-bd/UVA-Arena/releases/download/1.7/UVA_Arena_1.7_setup.exe)
2. [Download Setup File (SourceForge Mirror)](https://sourceforge.net/projects/uvaarena/files/UVA%20Arena%201.7/UVA_Arena_1.7.exe/download)

##### Version 1.6 [Fix 1]
1. [Download Setup file (EXE File : 4.34MB)](http://www.mediafire.com/download/kh7he74cceuxu3j/UVA_Arena_1.6.1.exe)     
2. [Download Setup file with problem archive (EXE File: 336MB)](http://www.mediafire.com/download/c3zi0zz7e18c98l/UVA_Arena_1.6_archive.exe) 

##### Version 1.5   
1. [Download Setup file (EXE File : 4.33MB)](https://github.com/dipu-bd/UVA-Arena/blob/master/Setup/Windows/UVA_Arena_1.5_x86_win.exe?raw=true)     

##### Version 1.4   
1. [Download Setup file (EXE File : 4.48MB)](https://github.com/dipu-bd/UVA-Arena/blob/master/Setup/Windows/UVA_Arena_1.4_x86_win.exe?raw=true)     

##### Version 1.2
1. [Download Setup File (EXE File : 1.5MB)](https://github.com/dipu-bd/UVA-Arena/blob/master/Setup/Windows/UVA%20Arena%201.2.exe?raw=true)     
2. [Download Setup File with Problem Descriptions (ZIP File : 238MB)](http://sourceforge.net/projects/uvaarena/files/UVA%20Arena%201.2/UVA%20Arena%201.2%20full.zip/download)     

##### Version 1.1
1. [For All Platforms](https://github.com/dipu-bd/UVA-Arena/blob/master/Setup/Windows/UVA_Arena_1.1.exe?raw=true)
	
</details>
	  
### Short Info
Main target of this software is to provide a useful and informative tool to the users to help them with their programming practice. Today programming has become a very competitive field. You need to keep track of your progress daily, learn new things, and of-course solve new problems as fast as you can. Problem picking and managing your codes is a time consuming process. You can minimize this wasting of time as much as possible using **UVA Arena**.  

Almost every features of [uHunt](http://uhunt.felix-halim.net/) has been included in this software. It also has many other useful features.   

This is a software for heavy users and serious programmers. It might seem too much to take at first glance. But once you get used to it, I think you will find it very easy and helpful.  

### Getting Help
You will find the wiki page for this software [here](https://github.com/dipu-bd/UVA-Arena/wiki)  

If you need to report any bugs or suggest any new feature post it [here](https://github.com/dipu-bd/UVA-Arena/issues)  

For any other information contact me at <dipu.sudipta@gmail.com>  

### List of features 
Here is a brief list of the features that this software provides:    
* Problem Viewer (Both HTML and PDF)
* Code Editor (C, C++ and Java)
* Judge Status
* User Statistics with graphs
* World Rank viewer
* Compare between users
* uDebug browser  

#### Screen Shots 
[![Whole](https://raw.githubusercontent.com/dipu-bd/UVA-Arena/master/Images/wiki/_all_.png)](https://raw.githubusercontent.com/dipu-bd/UVA-Arena/master/Images/wiki/_all_.png)  

### Licence Information
`This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License, version 3.0 as published by the Free Software Foundation.`  

`This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.`  

You should have received a copy of the GNU General Public License along with this program. If no see <http://www.gnu.org/licenses/>   

### Dependent Projects  
This software uses controls from the following sources-  
* [uHunt API](http://uhunt.felix-halim.net/api) for user's information and problem database and latest judge status. 
* For parsing and editing HTML files, [HTML Agility Pack](http://htmlagilitypack.codeplex.com/). 
* [NewtonSoft.JSON](http://james.newtonking.com/json) to parse [JSON](http://en.wikipedia.org/wiki/JSON) data file. 
* [Object List View](http://objectlistview.sourceforge.net/cs/index.html) is the _most used and notable control_. 
* Second most used is [Fast Colored Text Box](https://github.com/PavelTorgashov/FastColoredTextBox) for Syntax Highlighting and Code Editing.  
* To plot graphs in user progress tracker [ZedGraph](http://sourceforge.net/projects/zedgraph/) was used.  
* Main icon is a licenced as "Free for non-commercial use". Downloaded from [here](http://www.iconarchive.com/show/stark-icons-by-fruityth1ng/Applications-icon.html)  
* Icon resources are mostly open-source and many of them are downloaded from [IconArchive](http://www.iconarchive.com/). I couldn't look up the licences for all icons. Please inform me if any icon here is under strict copyright protection.
* Setup project [NSIS (Nullsoft Scriptable Install System)](http://nsis.sourceforge.net/Main_Page)

### Publishing

- Install [NSIS](https://nsis.sourceforge.io/Download).
- Publish with __Visual Studio__ to `setup/publish` folder.
- Run `makensis .\installer.nsi` inside `setup` folder.

## Developer  
> __Sudipto Chandra Dipu__  
> <dipu.sudipta@gmail.com> 
