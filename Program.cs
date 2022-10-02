//new Repl().Start();

void TestLetStatements() {
  var input = @"
    let x = 5;
    let y = 10;
    let foobar = 838383;
  ";

  var l = new Lexer(input);
  var p = l.New();
  var program = p.ParseProgram();

  p.Errors().ForEach(delegate(string error) {
    System.Console.WriteLine("parser error: {0}", error);
    return;
  });

  if (program == null) {
    Console.WriteLine("no program");
    return;
  }
  else if (program.Statements.Count != 3) {
    Console.WriteLine("invalid statements count. expected= {0} got= {1}", 3, program.Statements.Count);
    return;
  }

  var expectedList = new string[] {"x", "y", "foobar"};

  for (int i = 0; i < expectedList.Length; i++)
  {
    var s = program.Statements[i];
    var expected = expectedList[i];

    if (!testLetStatement(s, expected)) {
      return;
    }

    var ls = (LetStatement)s;

    System.Console.WriteLine("{0} {1} {2}", ls.token.Literal, ls.name.TokenLiteral(), ls.value.TokenLiteral());
  }

  System.Console.WriteLine("done.");
}

bool testLetStatement(Statement s, string name) {
  if (s.TokenLiteral() != "let") {
    System.Console.WriteLine("s.TokenLiteral() not 'let'. got={0}", s.TokenLiteral());

    return false;
  }

  var letStmt = s as LetStatement;

  if (letStmt == null) {
    System.Console.WriteLine("s not LetStatement. got={0}", s.GetType().Name);

    return false;
  }
  
  if (letStmt.name?.value != name) {
    System.Console.WriteLine("let statemnt name not {0}. got={1}", name, letStmt.name?.value);

    return false;
  }

  if (letStmt.name.TokenLiteral() != name) {
    System.Console.WriteLine("let statement TokenLiteral() not {0}. got={1}", name, letStmt.name.TokenLiteral());

    return false;
  }

  return true;
}

TestLetStatements();
