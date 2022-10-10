//new Repl().Start();

void TestIndentifierExpression() {
  var input = @"
  true;
  false;
  let v = true;  
  ";

  var l = new Lexer(input);
  var p = l.New();
  var program = p.ParseProgram();

  p.Errors().ForEach(delegate(string error) {
    System.Console.WriteLine("parser error: {0}", error);
    
    return;
  });

  System.Console.WriteLine(program.String());
  System.Console.WriteLine(Token.EOF);
}
TestIndentifierExpression();

/*
void TestLetStatements() {
  var input = @"
    let x = 5;
    let y = 10;
    let foobar = 838383;
    return x;
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

  System.Console.WriteLine(program.String());

  System.Console.WriteLine(Token.EOF);
}

TestLetStatements();
*/
