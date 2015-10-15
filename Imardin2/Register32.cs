using System;

namespace libImardin2 {
	public class Register32 {

		public uint Value { get; set; }
		readonly public string		Name;
		readonly public Register16	LowWord;
		readonly public Register8L	LowByte;
		readonly public Register8H	HighByte;

		public Register32 (string name) {
			Name = name;
			LowWord		= new Register16 (this);
			LowByte		= new Register8L (LowWord);
			HighByte	= new Register8H (LowWord);
		}

		public Register32 (TargetRegister name)
			: this (Enum.GetName (typeof(TargetRegister), name)) {
		}

		public Register32 (TargetRegister name, uint value) : this (name) {
			Value = value;
		}
	}
}

