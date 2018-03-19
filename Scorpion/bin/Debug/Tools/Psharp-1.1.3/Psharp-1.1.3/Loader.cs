using System;
using System.Reflection;

// entry point for the user's Psharp application.

namespace JJC.Psharp.Lang {
	class Loader {
		static void Main(string[] args) {
			// args = new string[] { "Interpreter" };
			if( args.Length == 0 )
				args = new string[] { "Main" };
			Assembly a = System.Reflection.Assembly.Load( "Psharp" );
			a.GetType( "JJC.Psharp.Lang.PrologMain" ).GetMethod( "CallbackMain" ).
				Invoke( a.CreateInstance( "JJC.Psharp.Lang.PrologMain" ),
				new object[] { args,
								 Assembly.GetExecutingAssembly() } );
		}
	}
}
