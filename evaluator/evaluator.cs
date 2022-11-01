public class Evaluator {
  public IObject Eval(Node node, Environment env) {
    switch (node) {
      case _Program p: {
        return evalProgram(p, env);
      }
      case ExpressionStatement es: {
        if (es.expression == null) {
          return Objects.nullObj;
        }
        else {
          return Eval(es.expression, env);
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
          var right = Eval(e.right, env);

          if (isError(right)) {
            return right;
          }

          return evalPrefixExpression(e.op, right);
        }
      }
      case InfixExpression e: {
        if (e.right == null) {
          return Objects.nullObj;
        }

        var left = Eval(e.left, env);

        if (isError(left)) {
          return left;
        }

        var right = Eval(e.right, env);

        if (isError(right)) {
          return right;
        }

        return evalInfixExpression(e.op, left, right);
      }
      case BlockStatement s:
        return evalBlockStatments(s, env);
      case ReturnStatement s: {
        if (s.returnValue == null) {
          return Objects.nullObj;
        }
        else {
          var v = Eval(s.returnValue, env);

          if (isError(v)) {
            return v;
          }

          return new ReturnValue(v);
        }
      }
      case IfExpression e:
        return evalIfExpression(e, env);
      case LetStatement s: {
        if (s.value == null) {
          return newError($"let statement has no value: {s.name}");
        }

        var val = Eval(s.value, env);

        if (isError(val)) {
          return val;
        }

        env.Set(s.name.value, val);

        return val;
      }
      case Identifier i:
        return evalIdentifier(i, env);
      default:
        return Objects.nullObj;
    }
  }

  IObject evalProgram(_Program p, Environment env) {
    IObject result = Objects.nullObj;

    foreach (var s in p.Statements)
    {
      result = Eval(s, env);

      if (result is ReturnValue r) {
        return r.value;
      }
      else if (result is Error e) {
        return e;
      }
    }

    return result;
  }

  Error newError(string s) {
    return new Error(s);
  }

  bool isError(IObject obj) {
    if (obj != Objects.nullObj) {
      return obj.Type() == IObject.ERROR_OBJ;
    }

    return false;
  }

  IObject evalPrefixExpression(string op, IObject right) {
    return op switch {
      "!" => evalBangOperatorExpression(right),
      "-" => evalMinusPrefixOperatorExpression(right),
      _ => newError($"unknown operator: {op} {right.Type()}"),
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
      return newError($"unknown operator: -{right.Type()}");
    }
  }

  IObject evalInfixExpression(string op, IObject left, IObject right) {
    if ((left is Integer l) && (right is Integer r)) {
      return evalIntegerInfixExpression(op, l, r);
    }
    else if (left.Type() != right.Type()) {
      return newError($"type mismatch: {left.Type()} {op} {right.Type()}");
    }
    else {
      return op switch {
        "==" when left == right => Objects.trueObj,
        "==" when left != right => Objects.falseObj,
        "!=" when left != right => Objects.trueObj,
        "!=" when left == right => Objects.falseObj,
        _ => newError($"unknown operator: {left.Type()} {op} {right.Type()}"),
      };
    }
  }

  IObject evalBlockStatments(BlockStatement bs, Environment env) {
    IObject result = Objects.nullObj;

    foreach (var s in bs.statements)
    {
      result = Eval(s, env);

      if (result != Objects.nullObj) {
        var rt = result.Type();

        if (rt == IObject.NULL_OBJ || rt == IObject.ERROR_OBJ) {
          return result;
        }
      }
    }

    return result;
  }

  IObject evalIfExpression(IfExpression e, Environment env) {
    bool isTruthy(IObject obj) {
      return obj switch {
        Null => false,
        Bool when obj == Objects.falseObj => false,
        _ => true,
      };
    }

    var condition = Eval(e.condition, env);

    if (isError(condition)) {
      return condition;
    }

    if (isTruthy(condition)) {
      return Eval(e.consequence, env);
    }
    else if (e.alternative != null) {
      return Eval(e.alternative, env);
    }
    else {
      return Objects.nullObj;
    }
  }

  IObject evalIdentifier(Identifier node, Environment env) {
    var val = env.Get(node.value);

    if (val == null) {
      return newError($"identifier not found: {node.value}");
    }

    return val;
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
      _ => newError($"unknown operator: {left.Type()} {op} {right.Type()}"),
    };
  }
}