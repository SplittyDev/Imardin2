using System;

namespace imc {
	public class GenericValueNode : ASTNode {
		public object Value;
	}

	public class GenericTargetNode : ASTNode {
		public ASTNode Target;
	}
}

