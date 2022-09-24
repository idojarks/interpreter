global using TokenType = System.String;

public class token
{
  static public string ILLEGAL = "ILLEGAL";
  static public string EOF = "EOF";

  static public string IDENT = "IDENT";
  static public string INT = "INT";

  static public string ASSIGN = "=";
  static public string PLUS = "+";

  static public string COMMA = ",";
  static public string SEMICOLON = ";";

  static public string LPAREN = "(";
  static public string RPAREN = ")";
  static public string LBRACE = "{";
  static public string RBRACE = "}";

  static public string FUNCTION = "FUNCTION";
  static public string LET = "LET";

  static Dictionary<string, TokenType> keywords = new();
  
  static token() {
    keywords.Add("fn", token.FUNCTION);
    keywords.Add("let", token.LET);
  }

  static public TokenType LookupIdent(string ident) {
    TokenType? tokenType;

    if (keywords.TryGetValue(ident, out tokenType))
    {
      return tokenType;
    }
    
    return token.IDENT;
  }

  public class Token {
    public TokenType Type = "";
    public string Literal = "";

    public Token(TokenType tokenType, string literal) {
      this.Type = tokenType;
      this.Literal = literal;
    }
  }  
}
