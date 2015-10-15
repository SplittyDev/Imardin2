using System;
using System.Collections.Generic;
using System.Text;
using libImardin2;

namespace libImardin2 {
	public class Lexer {
		
		readonly string source;
		readonly List<Token> tokens;
		int pos = -1;

		public Lexer (string input) {
			tokens = new List<Token> ();
			source = input;
		}

		public IEnumerable<Token> Scan () {
			while (CanAdvance ()) {
				EatWhitespace ();
				if (!CanAdvance ())
					break;
				if (char.IsLetter (Peek ()))
					ReadIdentOrLabel ();
				if (char.IsDigit (Peek ()))
					ReadNumber ();
				switch (Peek ()) {
				case ',':
					tokens.Add (new Token (TokenType.Comma, Read ()));
					break;
				case '%':
					Read ();
					ReadRegister ();
					break;
				case '$':
					Read ();
					ReadLabelOrRegisterReference ();
					break;
				case '#':
					Read ();
					ReadComment ();
					break;
				case '.':
					Read ();
					ReadPragma ();
					break;
				}
			}
			return tokens;
		}

		void ReadComment () {
			while (CanAdvance () && Peek () != '\n')
				Read ();
		}

		void ReadNumber (bool address = false) {
			const string hexchars = "abcdefABCDEF";
			var accum = new StringBuilder ();
			bool readhex = false;
			if (CanAdvance (2) && Peek () == '0' && Peek (2) == 'x') {
				Read (2);
				readhex = true;
			}
			if (readhex)
				while (CanAdvance () && char.IsDigit (Peek ())
					|| hexchars.Contains (Peek ().ToString ()))
					accum.Append (Read ());
			else
				while (CanAdvance () && char.IsDigit (Peek ()))
					accum.Append (Read ());
			Int64 buf;
			buf = readhex
				? Convert.ToInt64 (accum.ToString (), 16)
				: Convert.ToInt64 (accum.ToString ());
			Console.WriteLine ("[LEXER] {0}: {1}", address
				? "Address" : "Number", readhex ? "0x" + buf.ToString ("X")
				: buf.ToString ());
			/*
			if (address) {
				tokens.Add (new Token (TokenType.Address, (Int32)buf & 0x7FFFFFFF));
				return;
			} */
			if (buf <= byte.MaxValue)
				tokens.Add (new Token (TokenType.Int8, (byte)buf & 0xFF));
			else if (buf <= Int16.MaxValue)
				tokens.Add (new Token (TokenType.Int16, (Int16)buf & 0x7FFFF));
			else if (buf <= Int32.MaxValue)
				tokens.Add (new Token (TokenType.Int32, (Int32)buf & 0x7FFFFFFF));
			else if (buf <= Int64.MaxValue)
				tokens.Add (new Token (TokenType.Int64, buf & 0x7FFFFFFFFFFFFFFFL));
			else
				throw new Exception ("Integer too big!");
		}

		void ReadPragma () {
			var accum = new StringBuilder ();
			while (CanAdvance () && char.IsLetter (Peek ()))
				accum.Append (Read ());
			tokens.Add (new Token (TokenType.Pragma, accum.ToString ()));
			Console.WriteLine ("[LEXER] Pragma: {0}", accum);
		}

		void ReadRegister () {
			var accum = new StringBuilder ();
			while (CanAdvance () && char.IsLetter (Peek ()))
				accum.Append (Read ());
			tokens.Add (new Token (TokenType.Register, accum.ToString ()));
			Console.WriteLine ("[LEXER] Register: {0}", accum.ToString ().ToUpperInvariant ());
		}

		void ReadLabelOrRegisterReference () {
			var accum = new StringBuilder ();
			if (char.IsLower (Peek ()) || Peek () == '_')
				accum.Append (Read ());
			while (CanAdvance () && (char.IsLetter (Peek ()) || Peek () == '_'))
				accum.Append (Read ());
			if (Enum.IsDefined (typeof(TargetRegister), accum.ToString ().ToLowerInvariant ())) {
				tokens.Add (new Token (TokenType.RegisterReference, accum.ToString ()));
				Console.WriteLine ("[LEXER] RegisterRef: {0}", accum);
			} else {
				tokens.Add (new Token (TokenType.LabelReference, accum.ToString ()));
				Console.WriteLine ("[LEXER] LabelRef: {0}", accum);
			}
		}

		void ReadIdentOrLabel () {
			var accum = new StringBuilder ();
			accum.Append (Read ());
			while (CanAdvance () && (char.IsLetter (Peek ()) || Peek () == '_'))
				accum.Append (Read ());
			if (Peek () == ':') {
				Read ();
				tokens.Add (new Token (TokenType.LabelDefinition, accum.ToString ()));
				Console.WriteLine ("[LEXER] LabelDef: {0}", accum);
			} else {
				tokens.Add (new Token (TokenType.Identifier, accum.ToString ()));
				Console.WriteLine ("[LEXER] Identifier: {0}", accum);
			}
		}

		void EatWhitespace () {
			while (CanAdvance () && char.IsWhiteSpace (Peek ()))
				Read ();
		}

		char Read (int lookahead = 1) {
			pos += lookahead;
			return Peek (0);
		}

		char Peek (int lookahead = 1) {
			return !CanAdvance (lookahead) ? '\0' : source [pos + lookahead];
		}

		bool CanAdvance (int lookahead = 1) {
			return pos + lookahead < source.Length;
		}
	}
}

