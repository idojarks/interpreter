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

    public static token.Token newToken(TokenType tokenType, char ch) {
      return new token.Token(tokenType, Convert.ToString(ch));
    }

    public static bool isLetter(char ch) {
      return 'a' <= ch && ch <= 'z' || 'A' <= ch && ch <= 'Z' || ch == '_';
    }

    public void readChar() {
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

    public string readIdentifier() {
      var startPos = position;

      while (isLetter(ch))
      {
        readChar();
      }

      return input.Substring(startPos, position - startPos);
    }

    void skipWhiteSpace() {
      
    }

    public token.Token NextToken() {
      token.Token tok;



      switch (ch) {
        case '=':
          tok = newToken(token.ASSIGN, ch);
          break;
        case ';':
          tok = newToken(token.SEMICOLON, ch);
          break;
        case '(':
          tok = newToken(token.LPAREN, ch);
          break;
        case ')':
          tok = newToken(token.RPAREN, ch);
          break;
        case ',':
          tok = newToken(token.COMMA, ch);
          break;
        case '+':
          tok = newToken(token.PLUS, ch);
          break;
        case '{':
          tok = newToken(token.LBRACE, ch);
          break;
        case '}':
          tok = newToken(token.RBRACE, ch);
          break;
        case '\0':
          tok = new token.Token(token.EOF, "");
          break;
        default:
          if (isLetter(ch)) {
            var ident = readIdentifier();
            var tokenType = token.LookupIdent(ident);

            tok = new token.Token(tokenType, ident);

            return tok;
          }
          else {
            tok = newToken(token.ILLEGAL, ch);
          }
          break;
      }

      readChar();
      
      return tok;
    }
  }
}