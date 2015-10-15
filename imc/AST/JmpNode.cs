using System;

namespace imc {
	public class JmpNode : GenericTargetNode {
		public JmpNode (ASTNode target) {
			Target = target;
		}
	}
}

