using System;

namespace libImardin2 {

	[Flags]
	public enum LengthPrefix : byte {

		/// <summary>
		/// No length prefix.
		/// </summary>
		None = 0x00,

		/// <summary>
		/// First operand is 8 bits wide.
		/// </summary>
		f_8 = 1 << 0,

		/// <summary>
		/// First operand is 16 bits wide.
		/// </summary>
		f_16 = 1 << 1,

		/// <summary>
		/// First operand is 32 bits wide.
		/// </summary>
		f_32 = 1 << 2,

		f_reg = 1 << 3,

		/// <summary>
		/// Second operand is 8 bits wide.
		/// </summary>
		s_8 = 1 << 4,

		/// <summary>
		/// Second operand is 16 bits wide.
		/// </summary>
		s_16 = 1 << 5,

		/// <summary>
		/// Second operand is 32 bits wide.
		/// </summary>
		s_32 = 1 << 6,

		s_reg = 1 << 7,
	}
}

