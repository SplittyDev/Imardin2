using System;
using System.Linq;
using System.Collections.Generic;

namespace libImardin2 {
	public class CPU {

		public static object syncLock;
		public static CPU instance;
		public static CPU Instance {
			get {
				if (instance == null)
					lock (syncLock)
						if (instance == null)
							instance = new CPU ();
				return instance;
			}
		}

		public List<Register32> Registers;

		// General purpose registers
		public Register32 EAX, EBX, ECX, EDX;
		public Register32 EBP, ESP, ESI, EDI;

		private CPU (uint stackPointer = 0x1024) {
			Registers = new List<Register32> ();
			EAX = new Register32 (TargetRegister.eax, 0);
			EBX = new Register32 (TargetRegister.ebx, 0);
			ECX = new Register32 (TargetRegister.ecx, 0);
			EDX = new Register32 (TargetRegister.edx, 0);
			EBP = new Register32 (TargetRegister.ebp, 0);
			ESP = new Register32 (TargetRegister.esp, stackPointer);
			ESI = new Register32 (TargetRegister.esi, 0);
			EDI = new Register32 (TargetRegister.edi, 0);
			Registers.AddRange (new [] {
				EAX, EBX, ECX, EDX,
				EBP, ESP, ESI, EDI,
			});
		}

		public static CPU CreateNew (Address stackPointer) {
			instance = new CPU (stackPointer);
			return instance;
		}

		public static RegisterWidth GetRegisterWidth (TargetRegister reg) {
			if (reg >= TargetRegister.eax && reg <= TargetRegister.edi)
				return RegisterWidth.DWord;
			else if (reg >= TargetRegister.ax && reg <= TargetRegister.dx)
				return RegisterWidth.LowWord;
			else if (reg >= TargetRegister.ah && reg <= TargetRegister.dh)
				return RegisterWidth.HighByte;
			else if (reg >= TargetRegister.al && reg <= TargetRegister.dl)
				return RegisterWidth.LowByte;
			throw new Exception (string.Format ("Invalid register: {0}", reg));
		}

		public Register32 TranslateDWord (TargetRegister reg) {
			return Registers.First (register => Enum.GetName (typeof(TargetRegister), reg) == register.Name);
		}

		public Register16 TranslateLowWord (TargetRegister reg) {
			foreach (var reg32 in Registers) {
				if (reg32.LowWord.Name == Enum.GetName (typeof (TargetRegister), reg))
					return reg32.LowWord;
			}
			throw new Exception (string.Format ("Invalid register: {0}", reg));
		}

		public Register8H TranslateHighByte (TargetRegister reg) {
			foreach (var reg32 in Registers) {
				if (reg32.HighByte.Name == Enum.GetName (typeof (TargetRegister), reg))
					return reg32.HighByte;
			}
			throw new Exception (string.Format ("Invalid register: {0}", reg));
		}

		public Register8L TranslateLowByte (TargetRegister reg) {
			foreach (var reg32 in Registers) {
				if (reg32.LowByte.Name == Enum.GetName (typeof (TargetRegister), reg))
					return reg32.LowByte;
			}
			throw new Exception (string.Format ("Invalid register: {0}", reg));
		}
	}
}

