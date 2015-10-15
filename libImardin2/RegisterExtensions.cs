using System;

namespace libImardin2 {
	public static class RegisterExtensions {


		// Write DWORD
		public static void Write32 (this Register32 reg, uint value) {
			reg.Value = value;
		}

		// Write lower WORD
		public static void Write16 (this Register32 reg, short value) {
			reg.LowWord.Value = value;
		}

		// Write lower BYTE
		public static void Write8 (this Register32 reg, byte value) {
			reg.Write8Low (value);
		}

		// Write lower BYTE
		public static void Write8Low (this Register32 reg, byte value) {
			reg.LowByte.Value = value;
		}

		// Write higher WORD
		public static void Write8High (this Register32 reg, byte value) {
			reg.HighByte.Value = value;
		}
	}
}

