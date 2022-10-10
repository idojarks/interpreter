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

public class InvalidExpression : Expression {
  public InvalidExpression() : base(new Token(Token.ILLEGAL, Token.ILLEGAL)) {}

  public override string String()
  {
    return token.Literal;
  }
}

public class LetStatement : Statement {
  public Identifier name;
  public Expression? value;

  public LetStatement(Token t, Identifier i, Expression? e=null) : base(t) {
    name = i;
    value = e;
  }

  public override string String() {
    return $"{TokenLiteral()} {name.String()} = {value?.String()};";
  }
}

public class ReturnStatement : Statement {
  public Expression? returnValue;

  public ReturnStatement(Token t, Expression? e) : base(t) {
    returnValue = e;
  }

  public override string String() {
    return $"{TokenLiteral()} {returnValue?.String()};";
  }
}

public class ExpressionStatement : Statement {
  Expression? expression;

  public ExpressionStatement(Token t, Expression? e=null) : base(t) {
    expression = e;
  }

  public override string String() {
    return $"{expression?.String()}";
  }
}

public class BlockStatement : Statement {
  public List<Statement> statements = new();

  public BlockStatement() : base(new Token(Token.LBRACE, Token.LBRACE)) {}

  public override string String()
  {
    StringWriter w = new();

    statements.ForEach(delegate(Statement s) {
      w.Write(s.String());
    });

    return w.ToString();
  }
}

public class Identifier : Expression {
  public string value;

  public Identifier(Token t, string s) : base(t) {
    value = s;
  }

  public override string String() {
    return value;
  }
}

public class IntegerLiteral : Expression {
  public Int64 value;

  public IntegerLiteral(Token t, Int64 v) : base(t) {
    value = v;
  }

  public override string String() {
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

  public override string String() {
    return $"({op}{right?.String()})";
  }
}

public class InfixExpression : Expression {
  public Expression left;
  public string op;
  public Expression? right;

  public InfixExpression(Token t, Expression l, string o, Expression? r) : base(t) {
    left = l;
    op = o;
    right = r;
  }

  public override string String() {
    return $"({left.String()} {op} {right?.String()})";
  }
}

public class Boolean : Expression {
  public bool value;

  public Boolean(Token t, bool v) : base(t) {
    value = v;
  }

  public override string String()
  {
    return $"{token.Literal}";
  }
}

public class IfExpression : Expression {
  Expression condition;
  BlockStatement consequence;
  BlockStatement? alternative;

  public IfExpression(Token t, Expression cd, BlockStatement c, BlockStatement? a=null) : base(t) {
    condition = cd;
    consequence = c;
    alternative = a;
  }

  public override string String()
  {
    string? alt = null;

    if (alternative != null) {
      alt = $"else {alternative.String()}";
    }

    return $"if {condition.String()} {consequence.String()} {alt}";
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