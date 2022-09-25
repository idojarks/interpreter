global using TokenType = System.String;

public class Token
{
  static public string ILLEGAL = "ILLEGAL";
  static public string EOF = "EOF";

  static public string IDENT = "IDENT";
  static public string INT = "INT";

  static public string ASSIGN = "=";
  static public string PLUS = "+";
  static public string MINUS = "-";
  static public string BANG = "!";
  static public string ASTERISK = "*";
  static public string SLASH = "/";

  static public string LT = "<";
  static public string GT = ">";

  static public string COMMA = ",";
  static public string SEMICOLON = ";";

  static public string LPAREN = "(";
  static public string RPAREN = ")";
  static public string LBRACE = "{";
  static public string RBRACE = "}";

  static public string FUNCTION = "FUNCTION";
  static public string LET = "LET";
  static public string TRUE = "TRUE";
  static public string FALSE = "FALSE";
  static public string IF = "IF";
  static public string ELSE = "ELSE";
  static public string RETURN = "RETURN";

  static public string EQ = "==";
  static public string NOT_EQ = "!=";

  static Dictionary<string, TokenType> keywords = new();
  
  static Token() {
    keywords.Add("fn", FUNCTION);
    keywords.Add("let", LET);
    keywords.Add("true", TRUE);
    keywords.Add("false", FALSE);
    keywords.Add("if", IF);
    keywords.Add("else", ELSE);
    keywords.Add("return", RETURN);
  }

  static public TokenType LookupIdent(string ident) {
    TokenType? tokenType;

    if (keywords.TryGetValue(ident, out tokenType))
    {
      return tokenType;
    }
    
    return IDENT;
  }

  public TokenType Type = "";
  public string Literal = "";

  public Token(TokenType tokenType, string literal) {
    this.Type = tokenType;
    this.Literal = literal;
  }
}
