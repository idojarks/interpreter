public class Evaluator {
  public static IObject Eval(Node node) {
    switch (node) {
      case _Program p: {
        IObject? result = null;

        foreach (var s in p.Statements)
        {
          result = Eval(s);
        }

        if (result == null) {
          return new Null();
        }
        else {
          return result;
        }
      }
      case ExpressionStatement es: {
        if (es.expression == null) {
          return new Null();
        }
        else {
          return Eval(es.expression);
        }
      }
      case IntegerLiteral il:
        return new Integer(il.value);
      default:
        return new Null();
    }
  }
}