using System;

namespace libImardin2 {
	public class Memory {

		public static object syncLock;
		public static Memory instance;
		public static Memory Instance {
			get {
				if (instance == null)
					lock (syncLock)
						if (instance == null)
							instance = new Memory ();
				return instance;
			}
		}

		public delegate void MemoryFillPercentageChangedEventArgs (int perc);
		public event MemoryFillPercentageChangedEventArgs MemoryFillPercentageChanged;

		public byte[] memory;

		public byte this[byte i] {
			get { return memory[i]; }
			set { memory [i] = value; }
		}

		public Memory (UInt32 size = 2 * 1024 * 1024 /* 2Mib */) {
			memory = new byte[size];
			MemoryFillPercentageChanged += delegate { };
		}

		public static Memory CreateNew (uint size) {
			instance = new Memory (size);
			return instance;
		}

		public void ZeroFill () {
			MemoryFillPercentageChanged (0);
			const long limit = 4096;
			long memlen = memory.LongLength;
			float memlenfloat = (float)memlen;
			for (var i = 0L; i < memlen; i++) {
				if (i % limit == 0)
					MemoryFillPercentageChanged ((int)(((float)i / memlenfloat) * 100f));
				for (var j = 0L; j < limit; j++) {
					if (i + j == memlen)
						break;
					memory [i + j] = 0;
				}
				i += i + limit < memlen ? limit : limit - (memlen % i);
			}
			MemoryFillPercentageChanged (100);
		}
	}
}

