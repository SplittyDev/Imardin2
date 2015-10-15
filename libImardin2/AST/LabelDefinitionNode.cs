using System;

namespace libImardin2 {
	public class LabelDefinitionNode : ASTNode {

		public string Value;

		public LabelDefinitionNode (string value) {
			Value = value;
		}
	}
}

