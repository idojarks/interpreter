public abstract class Node {
  public Token token;

  public Node(Token t) {
    token = t;
  }

  public string TokenLiteral() {
    return token.Literal;
  }

  public abstract string String();
}

public abstract class Statement : Node {
  public Statement(Token t) : base(t) {}

  void statementNode() {}
}

public abstract class Expression : Node {
  public Expression(Token t) : base(t) {}

  void expressionNode() {}
}

public class LetStatement : Statement {
  public Identifier name;
  public Expression? value;

  public LetStatement(Token t, Identifier i, Expression? e=null) : base(t) {
    name = i;
    value = e;
  }

  override public string String() {
    return $"{TokenLiteral()} {name.String()} = {value?.String()};";
  }
}

public class ReturnStatement : Statement {
  public Expression? returnValue;

  public ReturnStatement(Token t, Expression? e) : base(t) {
    returnValue = e;
  }

  override public string String() {
    return $"{TokenLiteral()} {returnValue?.String()};";
  }
}

public class ExpressionStatement : Statement {
  Expression? expression;

  public ExpressionStatement(Token t, Expression? e=null) : base(t) {
    expression = e;
  }

  override public string String() {
    return $"{expression?.String()}";
  }
}

public class Identifier : Expression {
  public string value;

  public Identifier(Token t, string s) : base(t) {
    value = s;
  }

  override public string String() {
    return value;
  }
}

public class IntegerLiteral : Expression {
  public Int64 value;

  public IntegerLiteral(Token t, Int64 v) : base(t) {
    value = v;
  }

  override public string String() {
    return token.Literal;
  }
}

public class PrefixExpression : Expression {
  public string op;
  public Expression? right;

  public PrefixExpression(Token t, string o, Expression? e) : base(t) {
    op = o;
    right = e;
  }

  override public string String() {
    return $"({op}{right?.String()})";
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