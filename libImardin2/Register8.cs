using System;
using System.Linq;

namespace libImardin2 {
	public class Register8L {

		byte internal_value;
		public byte Value {
			get {
				return Base != null
					? (byte)(Base.Value & 0x00FF)
					: internal_value;
			}
			set {
				if (Base != null)
					Base.Value = (short)(Base.Value & 0xFF00 + value & 0x00FF);
				internal_value = value;
			}
		}

		readonly public string Name;
		readonly Register16 Base;

		public Register8L () {
			Name = string.Empty;
		}

		public Register8L (byte value) : this () {
			internal_value = value;
		}

		public Register8L (Register16 base_register) {
			Base = base_register;
			Name = string.Format ("{0}l", Base.Name.First ());
		}
	}

	public class Register8H {

		byte internal_value;
		public byte Value {
			get {
				return Base != null
					? (byte)(Base.Value & 0xFF00)
						: internal_value;
			}
			set {
				if (Base != null)
					Base.Value = (short)(Base.Value & 0x00FF + value & 0xFF00);
				internal_value = value;
			}
		}

		readonly public string Name;
		readonly Register16 Base;

		public Register8H () {
			Name = string.Empty;
		}

		public Register8H (byte value) : this () {
			internal_value = value;
		}

		public Register8H (Register16 base_register) {
			Base = base_register;
			Name = string.Format ("{0}h", Base.Name.First ());
		}
	}
}

