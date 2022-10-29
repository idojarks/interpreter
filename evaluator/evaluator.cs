public class Evaluator {
  public IObject Eval(Node node) {
    switch (node) {
      case _Program p: {
        return evalProgram(p);
      }
      case ExpressionStatement es: {
        if (es.expression == null) {
          return Objects.nullObj;
        }
        else {
          return Eval(es.expression);
        }
      }
      case IntegerLiteral il:
        return new Integer(il.value);
      case Boolean b: {
        if (b.value) {
          return Objects.trueObj;
        }
        else {
          return Objects.falseObj;
        }
      }
      case PrefixExpression e: {
        if (e.right == null) {
          return Objects.nullObj;
        }
        else {
          var right = Eval(e.right);

          return evalPrefixExpression(e.op, right);
        }
      }
      case InfixExpression e: {
        if (e.right == null) {
          return Objects.nullObj;
        }

        var left = Eval(e.left);
        var right = Eval(e.right);

        return evalInfixExpression(e.op, left, right);
      }
      case BlockStatement s:
        return evalBlockStatments(s);
      case ReturnStatement s: {
        if (s.returnValue == null) {
          return Objects.nullObj;
        }
        else {
          var v = Eval(s.returnValue);

          return new ReturnValue(v);
        }
      }
      case IfExpression e:
        return evalIfExpression(e);
      default:
        return Objects.nullObj;
    }
  }

  IObject evalProgram(_Program p) {
    IObject result = Objects.nullObj;

    foreach (var s in p.Statements)
    {
      result = Eval(s);

      if (result is ReturnValue r) {
        return r.value;
      }
    }

    return result;
  }

  IObject evalPrefixExpression(string op, IObject right) {
    return op switch {
      "!" => evalBangOperatorExpression(right),
      "-" => evalMinusPrefixOperatorExpression(right),
      _ => Objects.nullObj,
    };
  }

  IObject evalBangOperatorExpression(IObject right) {
    return right switch {
      Bool b when b._value == true => Objects.falseObj,
      Bool b when b._value == false => Objects.trueObj,
      Null => Objects.trueObj,
      _ => Objects.falseObj,
    };
  }

  IObject evalMinusPrefixOperatorExpression(IObject right) {
    if (right is Integer i) {
      return new Integer(-i.value);
    }
    else {
      return Objects.nullObj;
    }
  }

  IObject evalInfixExpression(string op, IObject left, IObject right) {
    if ((left is Integer l) && (right is Integer r)) {
      return evalIntegerInfixExpression(op, l, r);
    }
    else {
      return op switch {
        "==" when left == right => Objects.trueObj,
        "==" when left != right => Objects.falseObj,
        "!=" when left != right => Objects.trueObj,
        "!=" when left == right => Objects.falseObj,
        _ => Objects.nullObj,
      };
    }
  }

  IObject evalBlockStatments(BlockStatement bs) {
    IObject result = Objects.nullObj;

    foreach (var s in bs.statements)
    {
      result = Eval(s);

      if (result != Objects.nullObj && result.Type() == IObject.NULL_OBJ) {
        return result;
      }
    }

    return result;
  }

  IObject evalIfExpression(IfExpression e) {
    bool isTruthy(IObject obj) {
      return obj switch {
        Null => false,
        Bool when obj == Objects.falseObj => false,
        _ => true,
      };
    }

    var condition = Eval(e.condition);

    if (isTruthy(condition)) {
      return Eval(e.consequence);
    }
    else if (e.alternative != null) {
      return Eval(e.alternative);
    }
    else {
      return Objects.nullObj;
    }
  }

  IObject evalIntegerInfixExpression(string op, Integer left, Integer right) {
    return op switch {
      "+" => new Integer(left.value + right.value),
      "-" => new Integer(left.value - right.value),
      "*" => new Integer(left.value * right.value),
      "/" => new Integer(left.value / right.value),
      "<" when left.value < right.value => Objects.trueObj,
      "<" when left.value >= right.value => Objects.falseObj,
      ">" when left.value > right.value => Objects.trueObj,
      ">" when left.value <= right.value => Objects.falseObj,
      "==" when left.value == right.value => Objects.trueObj,
      "==" when left.value != right.value => Objects.falseObj,
      "!=" when left.value != right.value => Objects.trueObj,
      "!=" when left.value == right.value => Objects.falseObj,
      _ => Objects.nullObj,
    };
  }
}