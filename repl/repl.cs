public class Repl {
  static public string PROMPT = ">> ";

  public void Start() {
    while (true)
    {
      Console.Write(PROMPT);

      var line = Console.ReadLine();

      if (line == null || line == "exit") {
        return;
      }

      var l = new Lexer(line);
      var p = l.New();
      var program = p.ParseProgram();

      var errors = p.Errors();

      if (errors?.Count > 0) {
        errors.ForEach(delegate(string s) {
          System.Console.WriteLine(s);
        });

        return;
      }

      var eval = new Evaluator();
      var e = eval.Eval(program);

      System.Console.WriteLine(e.Inspect());
    }
  }
}
