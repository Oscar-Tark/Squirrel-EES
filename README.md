![IMG](/Art/Cropped.png)

**Please note that Scorpion has only been tested on Mono in Linux using .NET 4.7.2

[Scorpion IEE [Intelligent Execution Environment]]
------------------------------------------------

Is a framework that uses its own syntax in order to call functions of a defined c# accessibility level with a specific type of structure. You can also compile and link C# scripts to scorpion with functions you created in order to call them. Scorpion contains its own memory and databasing system. Each function call is done on an isolated thread.

[General syntax]
---------------

**Variables:**

`\*`   = Asterisk denotes a variable this refers to a defined variable that exists in the scorpion memory pool.

`*''` = Denotes a value to use as a variable, This allows you to use values without adding them to the scorpion memory pool such as something that is temporary.

(Example):

`var::*store`

`jsonget::*'http://ip.jsontest.com/', *store`

**Function calls:**

Are made with the following syntax:

`*return_var<<function::*var1, *var2...`

The first variable is a return variable. Any variable that the function returns will be stored there, if you would not like a return variable to be stored or the function you are calling does not return a variable you may ignore this section. 'function' denotes the name of the function to call. :: denotes that you are sending variables as agruments to the functions and subsequently all variables seperated by commas. any extra variables sent that do not interest the function call will be ignored such as a function call that requires 2 arguments but gets passed 5. The last 3 wil be ignored

(Example):

`listvars`

`var::*var1`

`varset::*var1, *'Scorpions are SO misunderstood..'`

**Running scripts:**

You may run external files as scripts which contain various scorpion function calls delimited by newlines. You may call a script by sending a path argument to the function 'runscript', it is reccomended to not use an extension for filenames:

`runscript::*'/home/user/myscript'`

You can also put other runscript calls inside of a script.

**WARNING!** Currently all function calls in a script will be called on the thread of the initial runscript function call. Multithreading for scripts is not yet supported.

**Compiling C# scripts:**

You may compile a C# script and load it into scorpion by namespace and class. Be warned that Scorpion only supports loading one class at a time for each assembly load.

Syntax:

`asmcompile::*path, *namespacedotclass, *reference, *reference...`

Example:

`asmcompile::*'/home/myhome/test.cs', *'test.Test', *'System.IO', *'System.Threading'`

**Loading C# scripts:**

In order to use any compiled C# script we must load them into Scorpion. This will allow us to call any function within a class. Please note that loading a C# library only supports loading one class at a time from all assemblies, this means that in order to load multiple classes from one assembly is currently not possible.

Syntax:

`asmload::*path, *namespacedotclass`

Example:

`asmload::*'/home/myhome/test.dll', *'test.Test'`

**Running C# script functions**

Functionaliy still in construction.
