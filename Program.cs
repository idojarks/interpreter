new Repl().Start();

/*
var input = @"let five = 52;
  if (five == 5) {
    return true;
  }
";

var tests = new List<Test>();
tests.Add(new Test(Token.LET, "let"));
tests.Add(new Test(Token.IDENT, "five"));
tests.Add(new Test(Token.ASSIGN, "="));
tests.Add(new Test(Token.INT, "52"));
tests.Add(new Test(Token.SEMICOLON, ";"));
tests.Add(new Test(Token.IF, "if"));
tests.Add(new Test(Token.LPAREN, "("));
tests.Add(new Test(Token.IDENT, "five"));
tests.Add(new Test(Token.EQ, "=="));
tests.Add(new Test(Token.INT, "5"));
tests.Add(new Test(Token.RPAREN, ")"));
tests.Add(new Test(Token.LBRACE, "{"));
tests.Add(new Test(Token.RETURN, "return"));
tests.Add(new Test(Token.TRUE, "true"));
tests.Add(new Test(Token.SEMICOLON, ";"));
tests.Add(new Test(Token.RBRACE, "}"));

var	l = new Lexer(input);
var i = 0;

foreach (var tt in tests)
{
  var tok = l.NextToken();

  if (tok.Type != tt.expectedType)
  {
    Console.WriteLine("tests[{0}] - token type wrong. expected={1}, got={2}", i, tt.expectedType, tok.Type);
    return;
  }

  if (tok.Literal != tt.expectedLiteral)
  {
    Console.WriteLine("tests[{0}] - literal wrong. expected={1}, got={2}", i, tt.expectedLiteral, tok.Literal);
    return;
  }

  ++i;
}

Console.WriteLine("done.");

class Test {
  public TokenType expectedType = "";
  public string expectedLiteral = "";

  public Test(TokenType tokenType, string literal) {
    expectedType = tokenType;
    expectedLiteral = literal;
  }
}
*/