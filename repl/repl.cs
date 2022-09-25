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

      var token = l.NextToken();

      while (token.Type != Token.EOF) {
        Console.WriteLine("type: {0}\tliteral: {1}", token.Type, token.Literal);

        token = l.NextToken();
      }
    }
  }
}
