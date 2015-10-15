using System;

namespace libImardin2 {
	public class InstructionImpl {
		readonly public string Name;

		public InstructionImpl (string name) {
			Name = name;
		}
	}

	public class InstrMov : InstructionImpl {
		public InstrMov () : base ("mov") { }
		public void Call (TargetRegister op1, TargetRegister op2) {
			
		}
	}
}

