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
			Console.WriteLine ("[PARSE] Full instruction");
			Console.WriteLine (tokens [pos].Type);

			// Identifier
			if (Match (TokenType.Identifier)) {
				Console.WriteLine ("[PARSE] Full instruction :: Identifier");
				var ident = tokens [pos].UnboxAs<string> ().ToLowerInvariant ();

				// Instruction
				if (Enum.IsDefined (typeof(Instruction), ident)) {
					Console.WriteLine ("[PARSE] Full instruction :: Identifier :: Instruction");
					return ParseInstruction ();
				}

				// Non-instruction identifier
				else {
					Console.WriteLine ("[PARSE] Full instruction :: Identifier :: Identifier");
					return ParseIdentifier ();
				}
			}

			// Label definition
			else if (Match (TokenType.LabelDefinition)) {
				Console.WriteLine ("[PARSE] Full instruction :: LabelDefinition");
				var label = Expect (TokenType.LabelDefinition).UnboxAs<string> ();
				return new LabelDefinitionNode (label);
			}

			ThrowUnexpected ();
			return new ASTNode ();	
		}

		public ASTNode ParseInstruction () {
			Console.WriteLine ("[PARSE] Instruction");

			var ident = Expect (TokenType.Identifier);
			switch (ident.UnboxAs<string> ()) {

			case "jmp":
				return ParseInstructionJmp ();
			
			// Other instructions (not implemented)
			default:
				Console.WriteLine ("Not implemented: Instruction '{0}'", ident.UnboxAs<string> ());
				ThrowUnexpected ();
				return new ASTNode ();
			}
		}

		public ASTNode ParseInstructionJmp () {
			JmpNode jmp;

			// Instruction target is label
			if (Match (TokenType.Identifier)) {
				Console.WriteLine ("[PARSE] Instruction :: Identifier");
				var ident = Expect (TokenType.Identifier).UnboxAs<string> ();
				Console.WriteLine ("[INFO] Instruction :: Identifier is Label '{0}'", ident);
				jmp = new JmpNode (new LabelTargetNode (ident));
				return jmp;
			}

			// Instruction target is address
			if (Match (TokenType.Int8)) {
				Console.WriteLine ("[PARSE] Instruction :: Int8");
				jmp = new JmpNode (new Int8Node (Expect (TokenType.Int8).UnboxAs<byte> ()));
				return jmp;
			} else if (Match (TokenType.Int16)) {
				Console.WriteLine ("[PARSE] Instruction :: Int16");
				jmp = new JmpNode (new Int16Node (Expect (TokenType.Int16).UnboxAs<Int16> ()));
				return jmp;
			} else if (Match (TokenType.Int32)) {
				Console.WriteLine ("[PARSE] Instruction :: Int32");
				jmp = new JmpNode (new Int32Node (Expect (TokenType.Int32).UnboxAs<Int32> ()));
				return jmp;
			} else if (Match (TokenType.Int64)) {
				Console.WriteLine ("[PARSE] Instruction :: Int64");
				jmp = new JmpNode (new Int64Node (Expect (TokenType.Int64).UnboxAs<Int64> ()));
				return jmp;
			}

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

