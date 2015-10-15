using System;
using System.IO;

namespace libImardin2 {
	public static class Emit {

		static BinaryWriter bitstream;

		public static void SetStream (BinaryWriter writer) {
			bitstream = writer;
		}

		public static void BuildInstruction (Instruction instr) {
			bitstream.Write ((byte)((byte)instr & 0xFF));
		}

		public static void BuildLengthPrefix (LengthPrefix prefix) {
			bitstream.Write ((byte)((byte)prefix & 0xFF));
		}

		public static void BuildRegister (TargetRegister reg) {
			bitstream.Write ((byte)((byte)reg & 0xFF));
		}

		public static void BuildAddress (Address addr) {
			bitstream.Write (addr.Value & 0x7FFFFFFF);
		}

		public static void instr (Instruction instr, TargetRegister op1, TargetRegister op2) {
			BuildInstruction (instr);
			BuildLengthPrefix (LengthPrefix.f_reg | LengthPrefix.s_reg);
			BuildRegister (op1);
			BuildRegister (op2);
		}

		public static void instr (Instruction instr, TargetRegister op1, byte op2) {
			BuildInstruction (instr);
			BuildLengthPrefix (LengthPrefix.f_reg | LengthPrefix.s_8);
			BuildRegister (op1);
			bitstream.Write (op2 & 0xFF);
		}

		public static void instr (Instruction instr, TargetRegister op1, Int16 op2) {
			if (CPU.GetRegisterWidth (op1) < RegisterWidth.LowWord)
				throw new Exception ("Can't fit 16-bit value into 8-bit register!");
			BuildInstruction (instr);
			BuildLengthPrefix (LengthPrefix.f_reg | LengthPrefix.s_16);
			BuildRegister (op1);
			bitstream.Write (op2 & 0x7FFFF);
		}

		public static void instr (Instruction instr, TargetRegister op1, Int32 op2) {
			if (CPU.GetRegisterWidth (op1) < RegisterWidth.DWord)
				throw new Exception ("Can't fit 32-bit value into 16-bit or 8-bit register!");
			BuildInstruction (instr);
			BuildLengthPrefix (LengthPrefix.f_reg | LengthPrefix.s_32);
			BuildRegister (op1);
			bitstream.Write (op2 & 0x7FFFFFFF);
		}

		public static void FlushStream () {
			bitstream.Flush ();
		}

		public static void CloseStream () {
			FlushStream ();
			bitstream.Close ();
		}
	}
}

