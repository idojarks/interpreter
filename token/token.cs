global using TokenType = System.String;

public class Token
{
  public const string ILLEGAL = "ILLEGAL";
  public const string EOF = "EOF";

  public const string IDENT = "IDENT";
  public const string INT = "INT";

  public const string ASSIGN = "=";
  public const string PLUS = "+";
  public const string MINUS = "-";
  public const string BANG = "!";
  public const string ASTERISK = "*";
  public const string SLASH = "/";

  public const string LT = "<";
  public const string GT = ">";

  public const string COMMA = ",";
  public const string SEMICOLON = ";";

  public const string LPAREN = "(";
  public const string RPAREN = ")";
  public const string LBRACE = "{";
  public const string RBRACE = "}";

  public const string FUNCTION = "FUNCTION";
  public const string LET = "LET";
  public const string TRUE = "TRUE";
  public const string FALSE = "FALSE";
  public const string IF = "IF";
  public const string ELSE = "ELSE";
  public const string RETURN = "RETURN";

  public const string EQ = "==";
  public const string NOT_EQ = "!=";

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
