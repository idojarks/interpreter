new Repl().Start();

/* void TestEvalIntegerExpression() {
  var input = @"
    5
  ";

  var l = new Lexer(input);
  var p = l.New();
  var program = p.ParseProgram();

  var v = Evaluator.Eval(program);
  System.Console.WriteLine($"{v.Inspect()} {v.Type()}");
}

TestEvalIntegerExpression();
 */
/* void TestIndentifierExpression() {
  var input = @"
    let f = add(2, 3*4, 5-6);
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
TestIndentifierExpression(); */

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
