using System;

namespace imc {
	public class LabelTargetNode : ASTNode {

		public string Value;

		public LabelTargetNode (string value) {
			Value = value;
		}
	}
}

