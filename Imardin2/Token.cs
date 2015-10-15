using System;

namespace libImardin2 {
	public enum TokenType {
		LabelDefinition,
		LabelReference,
		Identifier,
		Register,
		RegisterReference,
		Pragma,
		Int8,
		Int16,
		Int32,
		Int64,
		Comma,
	}

	public class Token {
		public TokenType Type { get; private set; }
		public object Value { get; private set; }

		public Token (TokenType type, object value) {
			Type = type;
			Value = value;
		}

		public T UnboxAs<T> () {
			return (T)Value;
		}
	}
}

