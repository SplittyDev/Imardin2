﻿using System;
using System.Collections.Generic;

namespace imc {
	public class ASTNode {
		public List<ASTNode> Children;

		public ASTNode () {
			Children = new List<ASTNode> ();
		}

		public void AddChild (ASTNode node) {
			Children.Add (node);
		}
	}
}

