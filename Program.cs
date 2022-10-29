//new Repl().Start();

var input = @"
    5*5;
    return 3;
    10*10;
  ";

  var l = new Lexer(input);
  var p = l.New();
  var program = p.ParseProgram();

  var v = new Evaluator().Eval(program);
  System.Console.WriteLine($"{v.Inspect()} {v.Type()}");