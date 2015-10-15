using System;

namespace libImardin2 {
	public class RegisterTargetNode : ASTNode {

		public string Value;

		public RegisterTargetNode (string value) {
			Value = value;
		}
	}
}

