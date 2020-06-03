[Scorpion IEE [Intelligent Execution Environment]]
------------------------------------------------

Is a framework that uses its own syntax in order to call functions of a defined c# accessibility level with a specific type of structure. You can also compile and link C# scripts to scorpion with functions you created in order to call them. Scorpion contains its own memory system and databasing system

[General syntax]
---------------

Variables:
**********

*   = Asterisk denotes a variable this refers to a defined variable that exists in the scorpion memory pool
*"" = Denotes a value to use as a variable, This allows you to use values without adding them to the scorpion memory pool such as something that is temporary

example:

var::*store

jsonget::*"http://ip.jsontest.com/", *store

Function calls:
***************

Are made with the following syntax:

*return_var<<function::*var1, *var2...

The first variable is a return variable. Any variable that the function returns will be stored there, if you would not like a return variable to be stored or the function you are calling does not return a variable you may ignore this section. 'function' denotes the name of the function to call. :: denotes that you are sending variables as agruments to the functions and subsequently all variables seperated by commas. any extra variables sent that do not interest the function call will be ignored such as a function call that requires 2 arguments but gets passed 5. The last 3 wil be ignored

examples:

listvars
var::*var1

varset::*var1, *"Scorpions are SO misunderstood.."

Running scripts:
****************

You may run external files as scripts which contain various scorpion function calls delimited by newlines. You may call a script by sending a path argument to the function 'runscript', it is reccomended to not use an extension for filenames:

runscript::*"/home/user/myscript"

You can also put other runscript calls inside of a script.
