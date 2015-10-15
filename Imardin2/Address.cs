using System;
using MiscUtil;

namespace libImardin2 {
	public class Address {

		public static readonly Address Last = UInt32.MaxValue;

		public virtual uint Value { get; private set; }

		public Address () {
			Value = 0;
		}

		public Address (uint val) {
			Value = val;
		}

		public static implicit operator uint (Address addr) {
			return addr.Value;
		}

		public static implicit operator Address (uint addr) {
			return new Address (addr);
		}
	}
}

