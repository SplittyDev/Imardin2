using System;
using System.Collections.Generic;
using libImardin2;

namespace libImardin2 {
	public class Parser {
		int pos;
		readonly List<Token> tokens;

		public Parser (List<Token> tokens) {
			this.tokens = tokens;
		}

		public ASTNode Parse () {
			var codeblock = new CodeContainerNode ();
			while (CanAdvance ())
				codeblock.AddChild (ParseFullInstruction ());
			return codeblock;
		}

		public ASTNode ParseFullInstruction () {
			//Console.WriteLine ("[PARSE] Full instruction");

			// Identifier
			if (Match (TokenType.Identifier)) {
				//Console.WriteLine ("[PARSE] Full instruction :: Identifier");
				var ident = tokens [pos].UnboxAs<string> ().ToLowerInvariant ();

				// Instruction
				if (Enum.IsDefined (typeof(Instruction), ident)) {
					//Console.WriteLine ("[PARSE] Full instruction :: Identifier :: Instruction");
					return ParseInstruction ();
				}

				// Non-instruction identifier
				else {
					//Console.WriteLine ("[PARSE] Full instruction :: Identifier :: Identifier");
					return ParseIdentifier ();
				}
			}

			// Label definition
			else if (Match (TokenType.LabelDefinition)) {
				//Console.WriteLine ("[PARSE] Full instruction :: LabelDefinition");
				var label = Expect (TokenType.LabelDefinition).UnboxAs<string> ();
				return new LabelDefinitionNode (label);
			}

			ThrowUnexpected ();
			return new ASTNode ();	
		}

		public ASTNode ParseInstruction () {
			//Console.WriteLine ("[PARSE] Instruction");

			var ident = Expect (TokenType.Identifier);
			switch (ident.UnboxAs<string> ()) {

			case "jmp":
				return ParseInstructionJmp ();

			case "mov":
				return ParseInstructionMov ();
			
			// Other instructions (not implemented)
			default:
				Console.WriteLine ("Not implemented: Instruction '{0}'", ident.UnboxAs<string> ());
				ThrowUnexpected ();
				return new ASTNode ();
			}
		}

		public ASTNode ParseInstructionJmp () {
			//Console.WriteLine ("[PARSE] Instruction :: jmp");
			var jmp = new GenericInstructionNode ("jmp");

			// Instruction target is label
			if (Match (TokenType.Identifier)) {
				//Console.WriteLine ("[PARSE] Instruction :: jmp :: Identifier");
				var ident = Expect (TokenType.Identifier).UnboxAs<string> ();
				//Console.WriteLine ("[INFO] Target label: '{0}'", ident);
				jmp.AddChild (new LabelTargetNode (ident));
				return jmp;
			}

			// Instruction target is address
			if (MatchIsNumber ()) {
				jmp.AddChild (ParseNumber ());
				return jmp;
			}

			ThrowUnexpected ();
			return new ASTNode ();
		}

		public ASTNode ParseInstructionMov () {
			//Console.WriteLine ("[PARSE] Instruction :: mov");
			var mov = new GenericInstructionNode ("mov");
			mov.AddChild (ParseOperandAny ());
			Expect (TokenType.Comma);
			mov.AddChild (ParseOperandAny ());
			return mov;
		}

		public ASTNode ParseOperandAny () {
			//Console.WriteLine ("[PARSE] Operand");

			// Operand is register
			if (Match (TokenType.Register)) {
				//Console.WriteLine ("[PARSE] Operand :: Register");
				var reg = Expect (TokenType.Register);
				return new RegisterTargetNode (reg.UnboxAs<string> ());
			}

			// Operand is label
			else if (Match (TokenType.Identifier)) {
				//Console.WriteLine ("[PARSE] Operand :: Identifier");
				var ident = Expect (TokenType.Identifier).UnboxAs<string> ();
				//Console.WriteLine ("[INFO] Target label: '{0}'", ident);
				return new LabelTargetNode (ident);
			}

			// Operand is number / address
			else if (MatchIsNumber ()) {
				//Console.WriteLine ("[PARSE] Operand :: Address");
				return ParseNumber ();
			}

			ThrowUnexpected ();
			return new ASTNode ();
		}

		public ASTNode ParseNumber () {
			if (Match (TokenType.Int8))
				return new Int8Node (Expect (TokenType.Int8).UnboxAs<byte> ());
			else if (Match (TokenType.Int16))
				return new Int16Node (Expect (TokenType.Int16).UnboxAs<Int16> ());
			else if (Match (TokenType.Int32))
				return new Int32Node (Expect (TokenType.Int32).UnboxAs<Int32> ());
			else if (Match (TokenType.Int64))
				return new Int64Node (Expect (TokenType.Int64).UnboxAs<Int64> ());
			ThrowUnexpected ();
			return new ASTNode ();
		}

		public ASTNode ParseIdentifier () {
			Expect (TokenType.Identifier);
			return null; // temporary
		}

		bool Match (TokenType type) {
			return CanAdvance () && tokens [pos].Type == type;
		}

		bool MatchIsNumber () {
			if (!CanAdvance ())
				return false;
			return
				Match (TokenType.Int8)	||
				Match (TokenType.Int16) ||
				Match (TokenType.Int32) ||
				Match (TokenType.Int64);
		}

		bool Accept (TokenType type) {
			if (Match (type)) {
				pos++;
				return true;
			}
			return false;
		}

		Token Expect (TokenType type) {
			if (!Match (type))
				ThrowExpected (type);
			return tokens [pos++];
		}

		bool CanAdvance (int count = 1) {
			return pos + count - 1 < tokens.Count;
		}

		void ThrowExpected (TokenType type) {
			var format = string.Format ("*** Expected: '{0}'; Got: '{1}'\n*** Value: '{2}'",
				type, tokens [pos].Type, tokens [pos].Value);
			throw new Exception (format);
		}

		void ThrowUnexpected () {
			var format = string.Format ("*** Unexpected: '{0}'", tokens [pos].Type);
			throw new Exception (format);
		}
	}
}

