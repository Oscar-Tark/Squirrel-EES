![IMG](/dotnet/img/Cropped.png)

[Scorpion IEE [Intelligent Execution Environment]:Quickstart Guide]
-------------------------------------------------------------------

Is a framework that uses its own syntax in order to call functions of a defined c# accessibility level with a specific type of structure. You can also compile and link C# scripts to scorpion with functions you created in order to call them. Scorpion contains its own memory and database system. Each function call is done on an isolated thread.

!NOTE: Call the `manual` function in order to view all available manuals and runnable functions for scorpion. You may use the `manual::*manual_name` function to view a manual.

[General syntax]
---------------

**Variables:**

`\*`   = Asterisk denotes a variable this refers to a defined variable that exists in the scorpion memory pool.

`*''` = Denotes a value to use as a variable, This allows you to use values without adding them to the scorpion memory pool such as something that is temporary.

`*f''` = Denotes a value variable which can be formatted. You may insert other variables into the current formatted value variable with the {(( ))} denotations.

(Example):

`var::*store >> varset::*store, *'My computer store'`

`output::*f'Hello {((store))}!`

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

- {&c} = ,
- {&v} = *
- {&q} = '
- {&r} = >>
- {&l} = <<
- {&d} = ::
- {&fl} = {((
- {&fr} = ))}

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

`var::*name >> varset::*name, *'Richard Stallman' >> output::*f'Hi {((name))}!. Let us play the GNU SONG!' >> exit`

[Scripts]
---------------

**Running Scorpion syntax scripts:**

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

[Loading C# scripts (mono_legacy only)]
---------------

In order to use any compiled C# script we must load them into Scorpion. This will allow us to call any function within a class. Please note that loading a C# library only supports loading one class at a time from all assemblies, this means that in order to load multiple classes from one assembly is currently not possible.

Syntax:

`asmload::*path, *namespacedotclass`

Example:

`asmload::*'/home/myhome/test.dll', *'test.Test'`

**Running C# script functions (mono_legacy only)**

You may now run a compiled function within a loaded assembly. This will allow you to call a function or a chain of functions. The beuty is that the sourcecoude can be modified recompiled and run on the fly. The current assembly must be removed from memory before loading in the new instance.

Syntax:

`asmcall::*path, *functionname, *arg, *arg...`

[Processes]
---------------

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

[Databases:XMLDB]
---------------

Databases are based of the XML standard and are simple encrypted files on your hard drive for storing static data. Scorpion's XMLDB allows you to get, set and query data.

**Creating a database:XMLDB**

Syntax:

`dbcreate::*dbpath, *password`

Example:

`dbcreate::*f'{((path))}/Databases/database.db'`

**Opening a database:XMLDB**

Databases can be opened by providing a path and a name which can be used to identify the database when working with it.

Syntax:

`dbopen::*dbname, *dbpath, *password`

Example:

`dbopen::*'mydatabase', *f'{((path))}/Databases/database.db', *'12345'`

**Closing a database:XMLDB**

Syntax:

`dbclose::*dbname`

Example:

`dbclose::*'mydatabase'`

**Saving a database:XMLDB**

Always remember to save changes before closing or reloading a database, if you do not all changes will be lost. It is reccomended to create a script that saves your database state every few minutes if the database is an active one.

Syntax:

`dbsave::*dbname, *password`

Example:

`dbsave::*'mydatabase', *'12345'`

**Reloading a database::XMLDB**

If you want to loose any changes made or want a fresh copy of a database in its previously last saved state you may reload a database.

Syntax:

`dbreload::*dbname, *password`

Example:

`dbreload::*'mydatabase', *'12345'`

**Querying data in XMLDB**

XMLDB is mostly reccomended as a static database for storing string data that you'd like to use later on. This can be HTML for a website which you can then process with formatted strings or image links. Data must be only in a string format. Saving images or videos may not work and will corrupt a database you may test it at your own perrill.

All data in XMLDB is represented by the following characteristics:

- tag : A group to which many data elements can belong to (Example: you as a person, a car or any class or classification of data)
- subtag : What does your data represent within the tag or classification of data (Example: your name or age)
- data : Contained data

**Getting data:XMLDB**

You may get data in XMLDB using either tags or/and subtags or/and data. If you do not have the value of one of them say you are fetching data relating to 'Tag:John Doe' but don't know what data is contained, you may pass the `*null` value as a parameter, all parameters are nullable. All queries return an array.

Syntax:

`*return_var<<dbget::*dbname, *data|OR NULL, *tag|OR NULL, *subtag|OR NULL`

Example:

`*result<<dbget::*dbname, *null, *'first_name', *null`

**Getting all data:XMLDB**

You may also get all available data from an XMLDB database, this will be returned as an array.

Syntax:

`*return_var::*dbname`

**Setting data:XMLDB**

You may insert but not update data in XMLDB, hence we reccomend using XMLDB mostly for static data such as containing a webpage.

Syntax:

`dbset::*dbname, *data, *tag|OR NULL, *subtag|OR NULL`

Example:

`dbset::*dbname, *'<p>Welcome!</p>', *'www.mywebsite.com', *'HTML'`

**Deleting data:XMLDB**

Data may be deleted in XMLDB using the query syntax allowing you to delete one or more elements partaining to a tag or subtag. Deleting all elements with a subtag deletes the subtag, deleting all elements with a tag deletes that tag.

Syntax:

`dbdelete::*dbname, *data|OR NULL, *tag|OR NULL, *subtag|OR NULL`

Example:

`dbdelete::*dbname, *null, *'first_name', *'John Doe'`

**Listing all open databases:XMLDB**

You may view a list of all open databases by using the following commands:

- `listdbs`
- `ld`

[Databases:MYSQL]
---------------

It is imperative that you have already set up a database and a user for your MYSQL instance in order to use Scorpion with it. Scorpion has a limited number of functionalities it can perform with MYSQL at the moment. This will be expanded in future releases to include fullscale MYSQL integration.

**Creating a new MYSQL connection string**

You may create a mysql connection string on your own and store it in a variable, type it out as a value variable. Scorpion allows you to automatically create a new mysql connection string and store it within a Scorpion variable as a returnable variable.

Syntax:

`*return_var<<mysqlcreatestring::*host|OR IP, *port|OR '3306', *database, *user, *password`

Example:

`result<<mysqlcreatestring::*'localhost', *'3306', *'mydatabase', *'user', *'12345'`

**Creating a new mysql table in the default format**

You may create a new mysql table from scorpion in it's default format or so in the XMLDB format. In order to simplify the way scorpion databases store data scorpion will automatically generate specific columns:

- id : An identifier for every row
- tag : A group to which many data elements can belong to (Example: you as a person, a car or any class or classification of data)
- subtag : What does your data represent within the tag or classification of data (Example: your name or age)
- data : Contained data
- token : Contains a scorpion based token for creating user based applications. The token can be used to verify that the user belongs to a specific user
