# FoxxyShutdownCMD
This small program can be used to invoke the shutdown command through Window's Task Scheduler with a few extra features, depending on parameters

A completed exe can be found in /bin/release
Standard procedure shuts down the computer immediately, but still warns of open programs. Parameters can be added in any order.

Valid parameters are as follows:
* **-h** hibernates the computer instead of shutting it down. Hibernation must be enabled in Windows, or else nothing will happen.
* **-force** forces the shutdown process regardless of any open programs
* **-t** sets a time interval in seconds before shutdown. eg "-t 60" waits 60 seconds before shutting down the computer
* **-f** uses the local text file "processes.txt" to define a list of processes. If any of those processes are found to be running, the computer will *not* shut down
* **-f <filename>** The parameter can also be defined with a full file path to a list of processes of the user's choosing, eg. "-f c:\processFilter\anyName.txt"

the process list is defined with a given process name on each line.

If you want to prevent shutdown if eg. Visual Studio, Steam, firefox or Notepad are running, type in the file like this:

 	

~~~~
  devenv
  Steam
  firefox
  notepad
 	

~~~~

file extensions like .exe are not required

Project is made for .NET 4.5 and should work on Windows 7 and upwards out of the box.
