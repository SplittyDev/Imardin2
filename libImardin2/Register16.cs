using System;
using System.Linq;

namespace libImardin2 {
	public class Register16 {

		short internal_value;
		public short Value {
			get {
				return Base != null
					? (short)(Base.Value & 0x0000FFFF)
					: internal_value;
			}
			set {
				if (Base != null)
					Base.Value = (uint)(Base.Value & 0xFFFF0000 + value & 0x0000FFFF);
				internal_value = value;
			}
		}

		readonly public string Name;
		readonly Register32 Base;

		public Register16 () {
			Name = string.Empty;
		}

		public Register16 (short value) : this () {
			internal_value = value;
		}

		public Register16 (Register32 base_register) {
			Base = base_register;
			Name = string.Format ("{0}x", Base.Name.Skip (1).First ());
		}
	}
}

