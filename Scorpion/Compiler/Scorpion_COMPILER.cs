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
            //::*path, *namespacedotclass, references...
            CompilerResults results = null;
            try
            {
                string path_ = (string)var_get(objects[0]);
                string dll_path = path_.Replace(".cs", ".dll");

                asmmessage("Compiling to path '" + dll_path + "");

                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters();

                parameters.GenerateInMemory = false;
                parameters.ReferencedAssemblies.Add("System.dll");

                for (int i = 2; i < objects.Count; i++)
                    parameters.ReferencedAssemblies.Add((string)var_get(objects[i]));

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
            catch(System.Exception e) { Console.WriteLine(e.Message + " ///" + e.StackTrace); }
            //((ArrayList)((ArrayList)Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(ref_)])[1]).Add(classInstance);
            //((ArrayList)((ArrayList)Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(ref_)])[0]).Add(namespace_class);*/

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public void asmcall(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *function, *parameters...

            Type program = Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(var_get(objects[0]))].GetType();
            MethodInfo mdf = program.GetMethod((string)var_get(objects[1]), BindingFlags.Public | BindingFlags.Instance);

            //Not doing a remove range due to var_get needed
            objects.RemoveRange(0,1);
            for (int i = 0; i < objects.Count; i++)
                objects[i] = var_get(objects[i]);

            mdf.Invoke(program, objects.ToArray());

            mdf = null;
            program = null;
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        public void listasm(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            foreach (string asm in Do_on.AL_ASSEMB_REF)
                Do_on.write_to_cui(asm);
            return;
        }

        private void asmmessage(string message)
        {
            Do_on.write_to_cui("[C# Compiler] " + message);
            message = null;
            return;
        }

        /*public void call_compiled_function(string Assembly_Name, Assembly assembly, string namespace_class, string function, string arguments)
        {
            //Type program = assembly.GetType(namespace_class);

            (Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(Assembly_Name)]).GetType().GetMethod(function, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Invoke(Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(Assembly_Name)], new object[0]);

            //((Type)(Do_on.AL_ASSEMB_PROG[Do_on.AL_ASSEMB_REF.IndexOf(Assembly_Name)])).GetMethod(function).Invoke(Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(Assembly_Name)], new object[0]);
            
            
            //object classInstance = Activator.CreateInstance(, null);
            
            /////////mdf.Invoke(Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(Assembly_Name)], new object[0]);

            //namespace_class = null;
            //-function = null;
            //arguments = null;

            //mdf = null;
            //classInstance = null;
            //program = null;
            //assembly = null;

            return;
        }

        public void create_instance(Assembly assembly, string namespace_class, string name)
        {
            Type program = assembly.GetType(namespace_class);

            MessageBox.Show(Activator.CreateInstance(assembly.GetType(namespace_class)).GetType().Name);

            Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(name)] = Activator.CreateInstance(assembly.GetType(namespace_class));
            Do_on.AL_ASSEMB_PROG[Do_on.AL_ASSEMB_REF.IndexOf(name)] = program;
            Do_on.write_to_cui("Created Assembly Instance: '" + namespace_class + "'");
            //((ArrayList)((ArrayList)Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(ref_)])[1]).Add(classInstance);
            //((ArrayList)((ArrayList)Do_on.AL_ASSEMB_INST[Do_on.AL_ASSEMB_REF.IndexOf(ref_)])[0]).Add(namespace_class);

            return;
        }

        //FIX
        /*public void import_assembly(string name)
        {
            Do_on.AL_ASSEMB_REF.Add(name);
            Do_on.AL_ASSEMB.Add(Assembly.LoadFile(Do_on.AL_DIRECTORIES[6] + name + ".dll"));
            Do_on.AL_ASSEMB_INST.Add("");
            Do_on.AL_ASSEMB_PROG.Add("");

            Do_on.write_to_cui("Imported Assembly");

            name = null;

            return;
        }*/
    }
}
