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

    public static ExpandoObject HelloFromScript( string s ) {
      dynamic obj = new ExpandoObject();
      obj.String = s;
      obj.Double = 42.5d;
      obj.Decimal = 500m;

      return obj;
    }

    public static void Main( string[] args ) {

      try {
        var module = new Module(

          @"
var s = foo( 'peter' ).String;
var double = foo( 'peter' ).Double
var decimal = foo( 'peter' ).Decimal;
console.log(s + ' ' + double + ' ' + decimal );"
);
        module.Context.DefineGetSetVariable( "foo", () => (Func<string,ExpandoObject>) HelloFromScript, o => { } );
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
