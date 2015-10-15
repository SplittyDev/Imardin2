using System;

namespace imc {
	public class Int8Node : ASTNode {

		public byte Value;

		public Int8Node (byte value) {
			Value = value;
		}
	}

	public class Int16Node : ASTNode {

		public Int16 Value;

		public Int16Node (Int16 value) {
			Value = value;
		}
	}

	public class Int32Node : ASTNode {

		public Int32 Value;

		public Int32Node (Int32 value) {
			Value = value;
		}
	}

	public class Int64Node : ASTNode {

		public Int64 Value;

		public Int64Node (Int64 value) {
			Value = value;
		}
	}
}

