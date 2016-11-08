using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NiL.JS;
using NiL.JS.BaseLibrary;
using NiL.JS.Core;

namespace jsnettest {
  public class Program {

    public static object HelloFromScript( string s ) {
      return new object[] { s, 42.5d, 500m };
    }

    public static void Main( string[] args ) {

      try {
        var module = new Module(

          @"
var s = foo( 'peter' )[0];
var double = foo( 'peter' )[1];
var decimal = foo( 'peter' )[2];
console.log(s + ' ' + double + ' ' + decimal );"
);
        module.Context.DefineGetSetVariable( "foo", () => (Func<string,object>) HelloFromScript, o => { } );
        module.Run();
      }
      catch ( JSException e ) {
        var syntaxError = e.Error.Value as SyntaxError;
        if ( syntaxError != null ) {
          Console.WriteLine( syntaxError.ToString() );
        }
        else {
          Console.WriteLine( "Unknown error: " + e );
        }
      }
    }
  }
}
