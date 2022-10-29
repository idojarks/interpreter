//new Repl().Start();

var input = @"
    5 + true;
    -true;
    true+false;
    if (10>1) {
      if (10>1) {
        return true+false;
      }
    }
  ";

  var l = new Lexer(input);
  var p = l.New();
  var program = p.ParseProgram();

  var v = new Evaluator().Eval(program);
  System.Console.WriteLine($"{v.Inspect()} {v.Type()}");