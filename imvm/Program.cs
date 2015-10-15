using System;
using Codeaddicts.libArgument;
using libImardin2;

namespace imvm {
	class MainClass {

		Options options;

		public static void Main (string[] args) {
			new MainClass ()._Main (args);	
		}

		public void _Main (string[] args) {
			options = ArgumentParser.Parse<Options> (args);
			options.Validate ();
			var mem = AllocMemory ();
			Console.WriteLine ("Preparing CPU...");
			var cpu = CPU.CreateNew (options.RealStackPointer);
			Console.WriteLine ("Idling...");
			Console.ReadLine ();
		}

		Memory AllocMemory () {
			var mem = Memory.CreateNew (options.RealMemSize);
			Console.Write ("Allocating memory...  ");
			mem.MemoryFillPercentageChanged += Mem_MemoryFillPercentageChanged;
			mem.ZeroFill ();
			mem.MemoryFillPercentageChanged -= Mem_MemoryFillPercentageChanged;
			Console.CursorLeft -= 1;
			Console.WriteLine ("Done");
			return mem;
		}

		readonly static char[] indicator = { '/', '-', '\\' };
		static int current_indicator;
		static void Mem_MemoryFillPercentageChanged (int perc) {
			Console.CursorLeft -= 1;
			Console.Write ("{1}", perc, indicator [current_indicator++ % indicator.Length]);
		}
	}
}
