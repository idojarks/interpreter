var input = "let five = 5;";

var tests = new List<Test>();
tests.Add(new Test(token.LET, "let"));
tests.Add(new Test(token.IDENT, "five"));
tests.Add(new Test(token.ASSIGN, "="));
tests.Add(new Test(token.INT, "5"));
tests.Add(new Test(token.SEMICOLON, ";"));

var	l = new lexer.Lexer(input);
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
    Console.WriteLine("tests[%d] - literal wrong. expected=%s, got=%s", i, tt.expectedLiteral, tok.Literal);
    return;
  }
}

class Test {
  public TokenType expectedType = "";
  public string expectedLiteral = "";

  public Test(TokenType tokenType, string literal) {
    expectedType = tokenType;
    expectedLiteral = literal;
  }
}