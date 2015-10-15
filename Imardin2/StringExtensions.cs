using System;
using System.Linq;

namespace libImardin2 {
	public static class StringExtensions {
		public static Address ToAddress (this string str) {
			uint dummy;
			if (!uint.TryParse (str, out dummy)) {
				char f = str.Last ();
				str = string.Join (string.Empty, str.Take (str.Count () - 1));
				switch (f) {
				case 'b':
				case 'B':
					return uint.Parse (str);
				case 'k':
				case 'K':
					return uint.Parse (str) * 1024;
				case 'm':
				case 'M':
					return uint.Parse (str) * 1024 * 1024;
				case 'g':
				case 'G':
					return uint.Parse (str) * 1024 * 1024 * 1024;
				default:
					throw new FormatException ("Invalid size suffix. Expected one of /[bkmg]/i.");
				}
			}
			return dummy;
		}
	}
}

