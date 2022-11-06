//new Repl().Start();

var input = @"
    ""foo"";
  ";

var l = new Lexer(input);
var p = l.New();
var program = p.ParseProgram();

/* p.Errors().ForEach(delegate(string s) {
  System.Console.WriteLine(s);
}); */

var env = new Environment();

var v = new Evaluator().Eval(program, env);
System.Console.WriteLine($"{v.Inspect()} {v.Type()}");