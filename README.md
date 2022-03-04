![IMG](/Art/Cropped.png)

**Please note that Scorpion has the following versions:

dotnet: works on .NET6.
mono_legacy (Not maintained): works on mono using .net 4.7.

[Scorpion IEE [Intelligent Execution Environment]]
------------------------------------------------

Is a framework that uses its own syntax in order to call functions of a defined c# accessibility level with a specific type of structure. You can also compile and link C# scripts to scorpion with functions you created in order to call them. Scorpion contains its own memory and databasing system. Each function call is done on an isolated thread.

[General syntax]
---------------

**Variables:**

`\*`   = Asterisk denotes a variable this refers to a defined variable that exists in the scorpion memory pool.

`*''` = Denotes a value to use as a variable, This allows you to use values without adding them to the scorpion memory pool such as something that is temporary.

`*f''` = Denotes a value variable which can be formatted. You may insert other variables into the current formatted value variable with the {[[ ]]} denotations.

(Example):

`var::*store >> varset::*store, *'My computer store'`

`output::*f'Hello {[[store]]}!`

**Arrays**

Arrays can replace an existing variable and transport it's value into index 0 of the array or as an empty array. Arrays may initialize a new variable and set it as an array.

`vararray::*name_or_existing, *if_existing_copy_value_into_new_array`

**Dictionaries**

Dictionaries act equally to arrays, except that they are arrays with key:value items. It a dictionary replaces a current array, there is no option to copy the current variable's value into the dictionary that substitutes it.

`vardictionary::*name_or_existing`

**String escape sequences**

Since certain symbols may be used for command execution, we have come up with some escape sequences that allow you to use those symbols within your strings of data similar to escape sequences in C such as \' for a single quote. Scorpion's escape sequences are abit different but the principal is the same. All escape sequences use the following format:

`{&escapee}`

where 'escapee' is a character denoting the type of escape.

Here they are:

> {&c} = ,
> {&v} = *
> {&q} = '
> {&r} = >>
> {&l} = <<
> {&d} = ::
> {&fl} = {[[
> {&fr} = ]]}

**Function calls:**

Are made with the following syntax:

`*return_var<<function::*var1, *var2...`

The first variable is a return variable. Any variable that the function returns will be shifted left and stored there, if you would not like a return variable to be stored or the function you are calling does not return a variable you may ignore this section. 'function' denotes the name of the function to call. :: denotes that you are sending variables as agruments to the functions and subsequently all variables seperated by commas. any extra variables sent that do not interest the function call will be ignored such as a function call that requires 2 arguments but gets passed 5. The last 3 wil be ignored

(Example):

`listvars`

`var::*var1`

`varset::*var1, *'Scorpions are SO misunderstood..'`

**One line multiple function calls:**

Scorpion allows you to run multiple functions in one line. Unlike other languages scorpions execution is left to right. To shift execution right use the >> symbol.

`var::*name >> varset::*name, *'Richard Stallman' >> output::*f'Hi {[[name]]}!. Let us play the GNU SONG!' >> exit`

**Running scripts:**

You may run external files as scripts which contain various scorpion function calls delimited by newlines. You may call a script by sending a path argument to the function 'scriptrun', it is reccomended to not use an extension for filenames:

`scriptrun::*'/home/user/myscript'`

You can also put other runscript calls inside of a script.

**WARNING!** Currently all function calls in a script will be called on the thread of the initial runscript function call. Multithreading for scripts is not yet supported.

**Compiling C# scripts (mono_legacy only):**

You may compile a C# script and load it into scorpion by namespace and class. Be warned that Scorpion only supports loading one class at a time for each assembly load.

Syntax:

`asmcompile::*path, *namespacedotclass, *reference, *reference...`

Example:

`asmcompile::*'/home/myhome/test.cs', *'test.Test', *'System.IO', *'System.Threading'`

**Loading C# scripts (mono_legacy only):**

In order to use any compiled C# script we must load them into Scorpion. This will allow us to call any function within a class. Please note that loading a C# library only supports loading one class at a time from all assemblies, this means that in order to load multiple classes from one assembly is currently not possible.

Syntax:

`asmload::*path, *namespacedotclass`

Example:

`asmload::*'/home/myhome/test.dll', *'test.Test'`

**Running C# script functions (mono_legacy only)**

You may now run a compiled function within a loaded assembly. This will allow you to call a function or a chain of functions. The beuty is that the sourcecoude can be modified recompiled and run on the fly. The current assembly must be removed from memory before loading in the new instance.

Syntax:

`asmcall::*path, *functionname, *arg, *arg...`

**Running external processes**

Running and controlling external processes is important for various use cases. You cannot control a process that is already running. You may create a process, view its output, kill a process or kill all running processes. Processes can run fully controlled by Scorpion or can be started as a new seperate process outsode of Scorpion Input insertion is coming soon!

**Starting an external process**

Syntax:

`process::*main_program_or_program_path, *arguments, *name, *isnotcontrolled`

Example:

`process::*'ping', *'127.0.0.1', *'mypingprocess', *false`

This example will run a ping process with the argument of '127.0.0.1'. false denotes that you would like the process to be controlled by Scorpion.

**Viewing process output**

Viewing processoutput is important. By default processes have their own stdout instances so that they will not print their output onto the main stdout seperating output and evading clutter. In order to see a processes stdout you may call the 'processio' function.

Syntax:

`processio::*name`

Example

`processio::*'mypingprocess'`

**Databases**

Functionality works, Manual comming soon.
