using System;

namespace libImardin2 {

	/* Instruction format:
	 * instruction	:: $Instruction
	 * register		:: $Register
	 * length_prefix:: $LengthPrefix
	 * str			:: "\"" /[a-z]{1}/i (/[a-z0-9]/i) "\""
	 * int			:: "$" [0-9] | "$" "0x" /[0-9a-f]+/i
	 * op			:: /(/"%" 'register | variable_int | 'labelref/)/ /\1/
	 * bytecode		:: instruction length_prefix op ("," op)
	 * 
	 * Examples:
	 * mov %eax, $0x20
	 * push $31
	 * */

	public enum Instruction : byte {

		#region Program flow
		jmp,
		#endregion

		#region Memory operations
		mov,
		push,
		pop,
		#endregion

		#region Memory mapped I/O
		inb,
		inw,
		inl,
		outb,
		outw,
		outl,
		#endregion

		#region CPU
		hlt,
		cli,
		sti,
		#endregion

		#region Pragma
		__resb,
		#endregion
	}
}

