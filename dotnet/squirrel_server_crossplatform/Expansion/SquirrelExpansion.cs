using System.Reflection;
using System.Collections;

namespace Scorpion
{
    partial class Librarian
    {
        public object csharp(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*instance<<::*path, *namespace.class, *method, *params...
            Assembly asm = Assembly.LoadFile((string)MemoryCore.varGet(objects[0]));
            Type type = asm.GetType((string)MemoryCore.varGet(objects[1]));
            object instance = Activator.CreateInstance(type);
            
            MethodInfo method = type.GetMethod((string)MemoryCore.varGet(objects[2]));
            objects.RemoveRange(0, 2);
            object result = method.Invoke(instance, new object[] { objects });
            return result;
        }
    }
}