
#Specification
A Lights-Out board consists of a grid of light bulbs, some of which are initially on. Clicking a bulb will toggle the state of the clicked bulb as well as the state of all bulbs adjacent (von Neumann neighborhood) to it. Goal of the game is to switch off all light bulbs.

Two new User Contorls
1. KeyControl
2. ResultControl

Two Classes
1. JsonLevel
2. LightOffMatrix


Two Initial Method
1. InitializeUserinteface();
2. InitBusinesslayer();


Two Event Handler
1. OnKeySwitch(); // Custom Event
2. WinTextBoard_MouseUp();


One Userinterface Update method
1. UpdateUserinterface()


Libraray 
1. GridHelpers Class
2. Newtonsoft.Json

Resource File
1. Click.wav
2. levels.json

