using System;
using System.Collections.Generic;
using System.Linq;
using Codeaddicts.libArgument.Attributes;

namespace imc {

	/// <summary>
	/// Commandline options.
	/// </summary>
	public class Options {

		/// <summary>
		/// Input file.
		/// </summary>
		[Argument ("-i", "/i")]
		[Docs ("Input imasm file.")]
		public string input;

		/// <summary>
		/// Output file.
		/// </summary>
		[Argument ("-o", "/o")]
		[Docs ("Output object file.")]
		public string output;

		public void Validate () {
			if (string.IsNullOrEmpty (input)) {
				Console.WriteLine ("[ERR ] No input file set. Nothing to compile.");
				Environment.Exit (1);
			}
			if (string.IsNullOrEmpty (output)) {
				Console.WriteLine ("[WARN] No output file set. Defaulting to out.bin.");
				output = "out.bin";
			}
		}
	}
}

