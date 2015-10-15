using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Codeaddicts.libArgument;
using libImardin2;

namespace imc {
	class MainClass {

		Options options;

		public static void Main (string[] args) {
			new MainClass ()._Main (args);	
		}

		public void _Main (string[] args) {
			options = ArgumentParser.Parse<Options> (args);
			options.Validate ();
			var input = File.ReadAllText (options.input);
			var lexer = new Lexer (input);
			var tokens = lexer.Scan ();
			var parser = new Parser (tokens.ToList ());
			var ast = parser.Parse ();
			Console.WriteLine ("[INFO] AST:");
			DumpAst (ast);
		}

		public static void DumpAst (ASTNode node, int depth = 0) {

			// Get target field
			var _target_field = node.GetType ()
				.GetFields (BindingFlags.GetField | BindingFlags.Instance | BindingFlags.Public)
				.FirstOrDefault (field => field.Name == "Target");

			// Get value field
			var _value_field = node.GetType ()
				.GetFields (BindingFlags.GetField | BindingFlags.Instance | BindingFlags.Public)
				.FirstOrDefault (field => field.Name == "Value");

			// Process target field
			if (_target_field != default (FieldInfo)) {
				string value = _target_field.GetValue (node).ToString ();
				Console.WriteLine ("{0} [TARGET] {1}", "".PadLeft (depth * 2, '-'), node);
				DumpAst (((GenericTargetNode)node).Target, depth + 1);
			}

			// Process value field
			else if (_value_field != default (FieldInfo)) {
				string value = _value_field.GetValue (node).ToString ();
				Console.WriteLine ("{0} [VALUE] {1} ({2})", "".PadLeft (depth * 2, '-'), node, value);
			}

			// Anything else
			else
				Console.WriteLine ("{0} {1}", "".PadLeft (depth, '-'), node);

			// Iterate over children
			foreach (var child in node.Children) {
				DumpAst (child, depth + 1);
			}
		}
	}
}
