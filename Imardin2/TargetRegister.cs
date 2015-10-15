using System;

namespace libImardin2 {
	public enum TargetRegister {

		// 32-bit registers
		eax,
		ebx,
		ecx,
		edx,
		ebp,
		esp,
		esi,
		edi,

		// 16-bit registers
		ax,
		bx,
		cx,
		dx,
		bp,
		sp,
		si,
		di,

		// 8bit registers (high)
		ah,
		bh,
		ch,
		dh,

		// 8-bit registers (low)
		al,
		bl,
		cl,
		dl,
	}
}

