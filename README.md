# This branch of UVA-Arena is still UNDER DEVELOPMENT

# UVA-Arena

![Facebook Icon](https://raw.githubusercontent.com/dipu-bd/UVA-Arena/master/Images/facebook.png) [Facbook Page](https://www.facebook.com/uvaarena?ref=github)   


[![UVA Arena Icon](https://raw.githubusercontent.com/dipu-bd/UVA-Arena/master/Images/Main.png)](https://raw.githubusercontent.com/dipu-bd/UVA-Arena/master/Images/Main.png)

**UVA Arena** (c) 2014, is a cross-platform, non-commercial, open source utility application to make problem solving easier for the programmers. It is based on popular [UVA Online Judge](http://uva.onlinejudge.org/) and uses APIs from [uHunt](http://uhunt.felix-halim.net/). 


### Short Info
Main target of this software is to provide a useful and informative tool to the users to help them with their programming practice. Today programming has become a very competitive field. You need to keep track of your progress daily, learn new things, and of-course solve new problems as fast as you can. Problem picking and managing your codes is a time consuming process. You can minimize this wasting of time as much as possible using **UVA Arena**.  

Almost every features of [uHunt](http://uhunt.felix-halim.net/) has been included in this software. It also has many other useful features.   

This is a software for heavy users and serious programmers. It might seem too much to take at first glance. But once you get used to it, I think you will find it very easy and helpful.  

### Getting Help
You will find the wiki page for this software [here](https://github.com/dipu-bd/UVA-Arena/wiki)  

If you need to report any bugs or suggest any new feature post it [here](https://github.com/dipu-bd/UVA-Arena/issues)  

For any other information contact me at <dipu.sudipta@gmail.com>  

###Licence Information
`This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License, version 3.0 as published by the Free Software Foundation.`  

`This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.`  

You should have received a copy of the GNU General Public License along with this program. If no see <http://www.gnu.org/licenses/>   

## Building from source

__You will only have to do the following once!__

To build UVA-Arena from source, you will need [Qt5](http://www.qt.io/), [git](https://git-scm.com/), and [CMake](http://www.cmake.org/). 

Clone this repository to a preferred directory, and update all submodules with this command:

```$ git submodule update --init --recursive```

This will download all of UVA-Arena's dependencies.

Now mupdf must be be built before proceeding. 

On windows, go to this directory:

__[UVA-Arena root]__/submodules/mupdf-qt/mupdf/platform/win32

and open the the solution file within the directory. Compile the solution and ignore anything about libcurl.

On Linux-based OSs, run the Makefile located in the root of mupdf.

With mupdf built, open up the CMake gui and set

"where is the source code: " to the root of the UVA-Arena project

and "Where to build the binaries: " to __[UVA-Arena project root]__/build

Next, click on the "Configure" button. CMake will let you know that the directory doesn't exist. Press OK, and then
another window will pop asking you to specify the generator for the project. On Windows, you'll most likely be developing with Visual Studio so select the version of Visual Studio that you have in the drop down list. If you're not using Visual Studio, then select the appropriate drop down item. On Linux-based OSs, simply select Unix Makefiles.

Leave everything else as is and click Finish. You will get an error prompting you to:

"Set Qt5_CMAKE_DIR to the directory containing the Qt5 cmake files"

This is a little shortcut UVA-Arena uses to have the user set their Qt5 directory that holds the CMake files that UVA-Arena needs. You can find your Qt5 CMake directory by navigating to your Qt5 installation directory, and navigating to this directory:

__[Qt root]__/Qt[Version]/[Version]/[configuration]/lib/cmake

For example, on a Windows computer, this directory might look like so:
C:\Qt\Qt5.4.1\5.4\msvc2013_opengl\lib\cmake

set this directory as the value to Qt5_CMAKE_DIR, and then in the CMake gui click configure again, and then click generate.
You will receive warnings in red text, but this is due to the mupdf-qt dependency. To ignore these warnings, click Options->Suppress dev Warnings in the CMake gui.

You should now have all the build files necessary to compile UVA-Arena within the build folder. 

### Trouble with CMake?

If you're having trouble with CMake and you have no idea why, follow these steps:

* Delete your build folder
* In the CMake gui, click File -> Delete cache

and then click Configure and generate again.
