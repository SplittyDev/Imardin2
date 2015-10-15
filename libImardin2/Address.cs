using System;
using MiscUtil;

namespace libImardin2 {
	public class Address {

		public static readonly Address Last = UInt32.MaxValue;

		public virtual UInt64 Value { get; private set; }

		public Address () {
			Value = 0;
		}

		public Address (UInt64 val) {
			Value = val;
		}

		public static implicit operator UInt64 (Address addr) {
			return addr.Value;
		}

		public static implicit operator Address (UInt64 addr) {
			return new Address (addr);
		}
	}
}

