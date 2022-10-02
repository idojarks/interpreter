public interface Node {
  string TokenLiteral();
}

public interface Statement : Node {
  void statementNode();
}

public interface Expression : Node {
  void expressionNode();
}

public class LetStatement : Statement {
  public Token token;
  public Identifier name;
  public Expression value;

  public LetStatement(Token t, Identifier i, Expression e) {
    token = t;
    name = i;
    value = e;
  }

  public void statementNode() {}

  public string TokenLiteral() {
    return token.Literal;
  }
}

public class Identifier : Expression {
  public Token token;
  public string value;

  public Identifier(Token t, string s) {
    token = t;
    value = s;
  }

  public void expressionNode() {}

  public string TokenLiteral() {
    return token.Literal;
  }
}

public class _Program {
  public List<Statement> Statements = new();

  public string TokenLiteral() {
    if (Statements.Count > 0) {
      return Statements[0].TokenLiteral();
    }
    else {
      return "";
    }
  }
}