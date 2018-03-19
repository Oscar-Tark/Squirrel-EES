using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;


namespace Scorpion.Hooking
{
    public class Hooker
    {
        Form1 Do_on;
        public Hooker(Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public void compile_(string path_, string name_, string namespace_dot_class, ref ArrayList references)
        {
            CompilerResults results = null;
            //try {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();

            parameters.GenerateInMemory = true;
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("System.Runtime.InteropServices.dll");
            parameters.ReferencedAssemblies.Add("System.Threading.dll");
            parameters.ReferencedAssemblies.Add("System.IO.dll");
            parameters.ReferencedAssemblies.Add("System.ComponentModel.dll");

            foreach (string s in references)
            {
                parameters.ReferencedAssemblies.Add(s);
            }

            //fnc.compile(*"F:\Work\One Platform\Tests\Fuck\hello.cs", *"hello", *"hello.hello")
            if (path_ != Do_on.types.S_NULL)
            {
                System.IO.File.Copy(path_, Do_on.AL_DIRECTORIES[5] + name_ + ".cs", true);
            }


            parameters.OutputAssembly = Do_on.AL_DIRECTORIES[6] + name_ + ".dll";

            /*if(!System.IO.File.Exists(Do_on.AL_DIRECTORIES[6] + name_ + ".dll"))
            {
                System.IO.FileStream fs = new System.IO.FileStream(Do_on.AL_DIRECTORIES[6] + name_ + ".dll", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                fs.Flush();
                fs.Close();
            }*/

            results = provider.CompileAssemblyFromFile(parameters, Do_on.AL_DIRECTORIES[5] + name_ + ".cs");

            Assembly assembly = results.CompiledAssembly;
            Type program = assembly.GetType(namespace_dot_class);
            /*}
            catch
            {
                foreach(CompilerError ce in results.Errors)
                {
                    Do_on.write_to_cui(">> Compile Error: " + ce.ErrorText + " AT LINE: " + ce.Line.ToString());
                }
            }*/

            /*MethodInfo[] mdinf = new MethodInfo[0];
            foreach (MethodInfo mdf in program.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
            {
                MessageBox.Show(mdf.Name);
            }*/

            path_ = null;

            return;
        }

        public void call_compiled_function(string Assembly_Name, Assembly assembly, string namespace_class, string function, string arguments)
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

        public void import_assembly(string name)
        {
            Do_on.AL_ASSEMB_REF.Add(name);
            Do_on.AL_ASSEMB.Add(Assembly.LoadFile(Do_on.AL_DIRECTORIES[6] + name + ".dll"));
            Do_on.AL_ASSEMB_INST.Add("");
            Do_on.AL_ASSEMB_PROG.Add("");

            Do_on.write_to_cui("Imported Assembly");

            name = null;

            return;
        }
    }
}
