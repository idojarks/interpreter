public interface Node {
  string TokenLiteral();
  string String();
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
  public Expression? value;

  public LetStatement(Token t, Identifier i, Expression? e=null) {
    token = t;
    name = i;
    value = e;
  }

  public void statementNode() {}

  public string TokenLiteral() {
    return token.Literal;
  }

  public string String() {
    return $"{TokenLiteral()} {name.String()} = {value?.String()};";
  }
}

public class ReturnStatement : Statement {
  public Token token;
  public Expression? returnValue;

  public ReturnStatement(Token t, Expression? e) {
    token = t;
    returnValue = e;
  }

  public void statementNode() {}

  public string TokenLiteral() {
    return token.Literal;
  }

  public string String() {
    return $"{TokenLiteral()} {returnValue?.String()};";
  }
}

public class ExpressionStatement : Statement {
  public Token token;
  Expression? expression;

  public ExpressionStatement(Token t, Expression? e=null) {
    token = t;
    expression = e;
  }

  public void statementNode() {}

  public string TokenLiteral() {
    return token.Literal;
  }

  public string String() {
    return $"{expression?.String()}";
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

  public string String() {
    return value;
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

  public string String() {
    StringWriter writer = new ();

    foreach (var item in Statements)
    {
      writer.WriteLine(item.String());
    }

    return writer.ToString();
  }
}