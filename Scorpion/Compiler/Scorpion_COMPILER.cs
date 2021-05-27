/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        public void asmcompile(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *namespacedotclass, *further_classes
            CompilerResults results = null;
            try
            {
                string path_ = (string)var_get(objects[0]);
                string dll_path = path_.Replace(".cs", ".dllx");

                asmmessage("Compiling to path '" + dll_path + "");

                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters();
                
                parameters.GenerateInMemory = false;

                if (objects.Count > 2)
                {
                    for (int i = 2; i < objects.Count; i++)
                    {
                        asmmessage("Adding assembly: " + (string)var_get(objects[i]));
                        parameters.ReferencedAssemblies.Add((string)var_get(objects[i]));
                    }
                }

                parameters.OutputAssembly = dll_path;
                results = provider.CompileAssemblyFromFile(parameters, path_);

                Assembly assembly = results.CompiledAssembly;

                asmmessage("Compile OK " + assembly.FullName + "' CLR version " + assembly.ImageRuntimeVersion);
                Type program = assembly.GetType((string)var_get(objects[1]));

                MethodInfo[] mdinf = new MethodInfo[0];
                foreach (MethodInfo mdf in program.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    asmmessage("Function OK '" + mdf.Name + "'\n");
            }
            catch
            {
                foreach (CompilerError ce in results.Errors)
                    asmmessage("ERROR: " + ce.ErrorText + " AT LINE: " + ce.Line.ToString());
                asmmessage("COMPILE FAILED");
            }

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public void asmload(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *namespace.class
            try
            {
                string asm_name = (string)var_get(objects[0]);
                string namespacedotclass = (string)var_get(objects[1]);
                Assembly asm = Assembly.LoadFile((string)var_get(objects[0]));

                asmmessage("Imported Assembly");

                Type program = asm.GetType(namespacedotclass);

                Do_on.AL_ASSEMB.Add(asm);
                Do_on.AL_ASSEMB_REF.Add(asm_name);
                Do_on.AL_ASSEMB_INST.Add(Activator.CreateInstance(asm.GetType(namespacedotclass)));
                Do_on.AL_ASSEMB_PROG.Add(program);
                asmmessage("Created Assembly Instance: '" + namespacedotclass + "'");
            }
            catch(Exception e) { Console.WriteLine(e.Message + " ///" + e.StackTrace); }

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public object asmcall(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *function, *parameters...
            object instance = Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(var_get(objects[0]))];
            Type program = instance.GetType();
            MethodInfo mdf = program.GetMethod((string)var_get(objects[1]), BindingFlags.Public | BindingFlags.Instance);

            //Not doing a remove range due to var_get needed
            objects.RemoveRange(0,1);
            for (int i = 0; i < objects.Count; i++)
                objects[i] = var_get(objects[i]);

            object return_value = null;
            try
            {
                if(mdf.GetParameters().Length > 0)
                    return_value = mdf.Invoke(instance, objects.ToArray());
                else
                    return_value = mdf.Invoke(instance, null);
            }
            catch(Exception e) { asmmessage(e.Message); }
            mdf = null;
            program = null;
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return var_create_return(ref return_value);
        }

        public void listasm(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            foreach (string asm in Do_on.AL_ASSEMB_REF)
                Do_on.write_to_cui(asm);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        private void asmmessage(string message)
        {
            Do_on.write_special("[C# Compiler] " + message);
            message = null;
            return;
        }
    }
}
