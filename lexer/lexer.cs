namespace lexer {
  public class Lexer {
    public Lexer(string input) {
      this.input = input;
      
      readChar();
    }

    public string input;
    public int position;
    public int readPosition;
    public char ch;

    public static Token newToken(TokenType tokenType, char ch) {
      return new Token(tokenType, Convert.ToString(ch));
    }

    bool isLetter(char ch) {
      return char.IsLetter(ch) || ch == '_';
    }

    bool isDigit(char ch) {
      return char.IsDigit(ch);
    }

    void readChar() {
      if (readPosition >= input.Length)
      {
        ch = '\0';
      }
      else
      {
        ch = input[readPosition];
      }

      position = readPosition;
      readPosition += 1;
    }

    string readIdentifier() {
      var startPos = position;

      while (isLetter(ch))
      {
        readChar();
      }

      return input.Substring(startPos, position - startPos);
    }

    string readInt() {
      var startPos = position;

      while (isDigit(ch)) {
        readChar();
      }
/*
      var intString = input.Substring(startPos, position - startPos);
      int.TryParse(intString, out int result);
      */

      return input.Substring(startPos, position - startPos);
    }

    void skipWhiteSpace() {
      while (string.IsNullOrWhiteSpace(ch.ToString()))
      {
        readChar();
      }
    }

    public Token NextToken() {
      Token tok;

      skipWhiteSpace();

      switch (ch) {
        case '=':
          tok = newToken(Token.ASSIGN, ch);
          break;
        case ';':
          tok = newToken(Token.SEMICOLON, ch);
          break;
        case '(':
          tok = newToken(Token.LPAREN, ch);
          break;
        case ')':
          tok = newToken(Token.RPAREN, ch);
          break;
        case ',':
          tok = newToken(Token.COMMA, ch);
          break;
        case '+':
          tok = newToken(Token.PLUS, ch);
          break;
        case '{':
          tok = newToken(Token.LBRACE, ch);
          break;
        case '}':
          tok = newToken(Token.RBRACE, ch);
          break;
        case '\0':
          tok = new Token(Token.EOF, "");
          break;
        default:
          if (isLetter(ch)) {
            var ident = readIdentifier();
            var tokenType = Token.LookupIdent(ident);

            tok = new Token(tokenType, ident);

            return tok;
          }
          else if (isDigit(ch)) {
            var result = readInt();

            tok = new Token(Token.INT, result);

            return tok;
          }
          else {
            tok = newToken(Token.ILLEGAL, ch);
          }
          break;
      }

      readChar();
      
      return tok;
    }
  }
}