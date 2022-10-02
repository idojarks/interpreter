public class Parser {
  Lexer l;
  Token curToken = new(Token.ILLEGAL, "");
  Token peekToken = new(Token.ILLEGAL, "");

  public Parser(Lexer l) {
    this.l = l;
  }

  public void nextToken() {
    curToken = peekToken;
    peekToken = l.NextToken();
  }

  Statement? parseStatement() {
    switch (curToken.Type) {
      case Token.LET:
        return parseLetStatement();
      default:
        return null;
    }
  }

  bool expectPeek(TokenType t) {
    if (peekToken.Type == t) {
      nextToken();
      
      return true;
    }
    else {
      System.Console.WriteLine("expectPeek expected= {0} got= {1}", t, peekToken.Type);

      return false;
    }
  }

  Statement? parseLetStatement() {
    var token = curToken;

    if (!expectPeek(Token.IDENT)) {
      return null;
    }

    var ident = curToken;

    if (!expectPeek(Token.ASSIGN)) {
      return null;
    }

    nextToken();
    var num = curToken;

    while (curToken.Type != Token.SEMICOLON) {
      nextToken();
    }

    return new LetStatement(
      token,
      new Identifier(ident, ident.Literal),
      new Identifier(num, num.Literal)
    );
  }

  public _Program ParseProgram() {
    var program = new _Program();

    while (curToken.Type != Token.EOF) {
      var s = parseStatement();

      if (s != null) {
        program.Statements.Add(s);
      }

      nextToken();
    }

    return program;
  }
}