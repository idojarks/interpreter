global using ObjectType = System.String;
using System.Text;

public interface IObject {
  ObjectType Type();
  string Inspect();

  const string INTEGER_OBJ = "INTEGER";
  const string BOOLEAN_OBJ = "BOOLEAN";
  const string NULL_OBJ = "NULL";
  const string RETURN_VALUE_OBJ = "RETURN_VALUE";
  const string ERROR_OBJ = "ERROR";
  const string FUNCTION_OBJ = "FUNCTION";
  const string STRING_OBJ = "STRING";
  const string BUILTIN_OBJ = "BUILTIN";
}

public class Objects {
  public static Bool trueObj = new Bool(true);
  public static Bool falseObj = new Bool(false);
  public static Null nullObj = new Null();
}

public class Integer : IObject {
  public Int64 value;

  public Integer(Int64 v) {
    value = v;
  }

  public string Inspect() {
    return $"{value}";
  }

  public ObjectType Type() {
    return IObject.INTEGER_OBJ;
  } 
}

public class StringObj : IObject {
  public string value;

  public StringObj(string s) {
    value = s;
  }

  public string Inspect() {
    return value;
  }

  public ObjectType Type() {
    return IObject.STRING_OBJ;
  } 
}

public class Bool : IObject {
  public bool value;

  public Bool(bool v) {
    value = v;
  }

  public string Inspect() {
    return $"{value.ToString()}";
  }

  public ObjectType Type() {
    return IObject.BOOLEAN_OBJ;
  }
}

public class Null : IObject {
  public string Inspect() {
    return "null";
  }

  public ObjectType Type() {
    return IObject.NULL_OBJ;
  }
}

public class ReturnValue : IObject {
  public IObject value = Objects.nullObj;

  public ReturnValue(IObject v) {
    value = v;
  }

  public ObjectType Type() {
    return IObject.RETURN_VALUE_OBJ;
  }

  public string Inspect() {
    return value.Inspect();
  }
}

public class Error : IObject {
  public string message = "";

  public Error(string s) {
    message = s;
  }

  public ObjectType Type() {
    return IObject.ERROR_OBJ;
  }

  public string Inspect() {
    return $"ERROR: {message}";
  }
}

public class Function : IObject {
  public List<Identifier>? parameters = null;
  public BlockStatement? body = null;
  public Environment env;

  public Function(List<Identifier>? p, BlockStatement? b, Environment e) {
    parameters = p;
    body = b;
    env = e;
  }

  public ObjectType Type() {
    return IObject.FUNCTION_OBJ;
  }

  public string Inspect() {
    var sb = new StringBuilder();

    if (parameters != null) {
      foreach (var item in parameters)
      {
        sb.Append(item.String());
      }
    }

    var bodyString = "{}";

    if (body?.statements.Count > 0) {
      bodyString = $"{{\n{body?.String()}\n}}";
    }

    return $"fn ({String.Join(", ", sb.ToString())}) {bodyString}";
  }
}

public class BuiltinFunction : IObject {
  public ObjectType Type() {
    return IObject.BUILTIN_OBJ;
  }

  public string Inspect() {
    return "builtin function";
  }
}