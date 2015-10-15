using System;
using System.Collections.Generic;
using System.Linq;
using Codeaddicts.libArgument.Attributes;
using libImardin2;

namespace imvm {

	/// <summary>
	/// Commandline options.
	/// </summary>
	public class Options {

		/// <summary>
		/// Input file.
		/// </summary>
		[Argument ("-i", "/i")]
		[Docs ("Input binary.")]
		public string input;

		[Argument ("-m", "--memory", "/m", "/memory-size")]
		[Docs ("Memory size.")]
		public string memorysize;

		[Argument ("-s", "--stack", "/s", "/stack")]
		[Docs ("Stack address. Use \".\" to use the last available byte.")]
		public string stackpointer;

		public UInt64 RealMemSize { get { return memorysize.ToAddress (); } }
		public UInt64 RealStackPointer { get { return stackpointer == "." ? (Address)RealMemSize : stackpointer.ToAddress (); } }

		public void Validate () {
			if (string.IsNullOrEmpty (memorysize)) {
				Console.WriteLine ("[WARN] Memory size is not set. Defaulting to 1M.");
				memorysize = "1M"; // 1Mib default memory
			} else
				Console.WriteLine ("[INFO] Memory size is {0} ({1}kb).", memorysize, RealMemSize / 1000);
			if (string.IsNullOrEmpty (stackpointer)) {
				Console.WriteLine ("[WARN] Stack address is not set. Defaulting to . (last address).");
			} else
				Console.WriteLine ("[INFO] Stack address is {0} (at 0x{1:X}).", stackpointer, RealStackPointer);
			if (string.IsNullOrEmpty (input))
				Console.WriteLine ("[WARN] No input file set. CPU will idle.");
		}
	}
}

