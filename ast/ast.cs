interface Node {
  string TokenLiteral();
}

interface Statement : Node {
  void statementNode();
}

interface Expression : Node {
  void expressionNode();
}

class _Program {
  Statement[] Statements = {};

  string TokenLiteral() {
    if (Statements.Length > 0) {
      return Statements[0].TokenLiteral();
    }
    else {
      return "";
    }
  }
}

class LetStatement : Statement {
  Token token = new (Token.LET, "let");
  Identifier? name;
  Expression? value;

  public void statementNode() {}

  public string TokenLiteral() {
    return token.Literal;
  }
}

class Identifier : Expression {
  Token token = new (Token.IDENT, "");
  string value = "";

  public void expressionNode() {}

  public string TokenLiteral() {
    return token.Literal;
  }
}