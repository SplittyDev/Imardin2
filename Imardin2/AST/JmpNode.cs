using System;

namespace libImardin2 {
	public class JmpNode : GenericTargetNode {
		public JmpNode (ASTNode target) {
			Target = target;
		}
	}
}

