//new Repl().Start();

var input = @"
let a = [1, 2+3, 4*2];
a[2]
";

var l = new Lexer(input);
var p = l.New();
var program = p.ParseProgram();

p.Errors().ForEach(delegate(string s) {
  System.Console.WriteLine(s);
});

var env = new Environment();

var v = new Evaluator().Eval(program, env);
System.Console.WriteLine($"{v.Inspect()} {v.Type()}");