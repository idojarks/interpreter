public class Evaluator {
  Bool trueObj = new Bool(true);
  Bool falseObj = new Bool(false);
  Null nullObj = new Null();

  public IObject Eval(Node node) {
    switch (node) {
      case _Program p: {
        IObject? result = null;

        foreach (var s in p.Statements)
        {
          result = Eval(s);
        }

        if (result == null) {
          return nullObj;
        }
        else {
          return result;
        }
      }
      case ExpressionStatement es: {
        if (es.expression == null) {
          return nullObj;
        }
        else {
          return Eval(es.expression);
        }
      }
      case IntegerLiteral il:
        return new Integer(il.value);
      case Boolean b: {
        if (b.value) {
          return trueObj;
        }
        else {
          return falseObj;
        }
      }
      case PrefixExpression e: {
        if (e.right == null) {
          return nullObj;
        }
        else {
          var right = Eval(e.right);

          return evalPrefixExpression(e.op, right);
        }
      }
      case InfixExpression e: {
        if (e.right == null) {
          return nullObj;
        }

        var left = Eval(e.left);
        var right = Eval(e.right);

        return evalInfixExpression(e.op, left, right);
      }
      default:
        return nullObj;
    }
  }

  IObject evalPrefixExpression(string op, IObject right) {
    return op switch {
      "!" => evalBangOperatorExpression(right),
      "-" => evalMinusPrefixOperatorExpression(right),
      _ => nullObj,
    };
  }

  IObject evalBangOperatorExpression(IObject right) {
    return right switch {
      Bool b when b._value == true => falseObj,
      Bool b when b._value == false => trueObj,
      Null => trueObj,
      _ => falseObj,
    };
  }

  IObject evalMinusPrefixOperatorExpression(IObject right) {
    if (right is Integer i) {
      return new Integer(-i.value);
    }
    else {
      return nullObj;
    }
  }

  IObject evalInfixExpression(string op, IObject left, IObject right) {
    if ((left is Integer l) && (right is Integer r)) {
      return evalIntegerInfixExpression(op, l, r);
    }
    else {
      return op switch {
        "==" when left == right => trueObj,
        "==" when left != right => falseObj,
        "!=" when left != right => trueObj,
        "!=" when left == right => falseObj,
        _ => nullObj,
      };
    }
  }

  IObject evalIntegerInfixExpression(string op, Integer left, Integer right) {
    return op switch {
      "+" => new Integer(left.value + right.value),
      "-" => new Integer(left.value - right.value),
      "*" => new Integer(left.value * right.value),
      "/" => new Integer(left.value / right.value),
      "<" when left.value < right.value => trueObj,
      "<" when left.value >= right.value => falseObj,
      ">" when left.value > right.value => trueObj,
      ">" when left.value <= right.value => falseObj,
      "==" when left.value == right.value => trueObj,
      "==" when left.value != right.value => falseObj,
      "!=" when left.value != right.value => trueObj,
      "!=" when left.value == right.value => falseObj,
      _ => nullObj,
    };
  }
}