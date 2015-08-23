
# UVA-Arena

![Facebook Icon](https://raw.githubusercontent.com/dipu-bd/UVA-Arena/master/Images/facebook.png) [Facbook Page](https://www.facebook.com/uvaarena?ref=github)

**UVA Arena** is a cross-platform, non-commercial, open source application made to help programmers become better programmers. UVA Arena uses the [UVA Online Judge](http://uva.onlinejudge.org/) and APIs from [uHunt](http://uhunt.felix-halim.net/) to provide programmers with lots of contest programming problems.

### What's different about this fork?

This fork of **UVA Arena** is an alternative to the [original **UVA Arena** application](https://github.com/dipu-bd/UVA-Arena) developed by [Sudipto Chandra](https://github.com/dipu-bd). Development on this fork concentrates on compatibility with all major operating systems. Cross-compatibility is achieved through the [Qt GUI framework](http://www.qt.io/developers/). This fork also attempts to provide a simple, modern GUI interface while maintaining most of the features available from the original UVA Arena application.

Screenshots:

![](http://i.imgur.com/mzS4Gm0.png)

![](http://i.imgur.com/minwAUc.png)

### Feature request

To request a feature, submit a new issue.

### Licence Information
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
