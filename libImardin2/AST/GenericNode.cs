using System;

namespace libImardin2 {
	public class GenericValueNode : ASTNode {
		public object Value;
	}

	public class GenericTargetNode : ASTNode {
		public ASTNode Target;
	}

	public class GenericInstructionNode : ASTNode {
		public string Value;

		public GenericInstructionNode (string value) {
			Value = value;
		}
	}
}

