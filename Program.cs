//new Repl().Start();

var input = @"
    let a = 4;
    a;
    b;
  ";

  var l = new Lexer(input);
  var p = l.New();
  var program = p.ParseProgram();
  var env = new Environment();

  var v = new Evaluator().Eval(program, env);
  System.Console.WriteLine($"{v.Inspect()} {v.Type()}");