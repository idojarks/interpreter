﻿var input = "let five = 52;";

var tests = new List<Test>();
tests.Add(new Test(Token.LET, "let"));
tests.Add(new Test(Token.IDENT, "five"));
tests.Add(new Test(Token.ASSIGN, "="));
tests.Add(new Test(Token.INT, "52"));
tests.Add(new Test(Token.SEMICOLON, ";"));

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