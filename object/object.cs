global using ObjectType = System.String;
using System.Text;

public interface IHashable {
  public HashKey HashKey();
}

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
  const string ARRAY_OBJ = "ARRAY";
  const string HASH_OBJ = "HASH";
}

public class Objects {
  public static Bool trueObj = new Bool(true);
  public static Bool falseObj = new Bool(false);
  public static Null nullObj = new Null();
}

public class Integer : IObject, IHashable {
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

  public HashKey HashKey() {
    return new HashKey(Type(), value.GetHashCode());
  }
}

public class StringObj : IObject, IHashable {
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

  public HashKey HashKey() {
    var hashCode = value.GetHashCode();
    return new HashKey(Type(), hashCode);
  }
}

public class Bool : IObject, IHashable {
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

  public HashKey HashKey() {
    Int64 v;

    if (value == true) {
      v = 1;
    }
    else {
      v = 0;
    }

    return new HashKey(Type(), v.GetHashCode());
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

public class Builtin : IObject {
  public delegate IObject BuiltinFunction(params IObject[] args);
  public BuiltinFunction fn;

  public Builtin(BuiltinFunction f) {
    fn = f;
  }

  public ObjectType Type() {
    return IObject.BUILTIN_OBJ;
  }

  public string Inspect() {
    return "builtin function";
  }
}

public class ArrayObj : IObject {
  public List<IObject> elements;

  public ArrayObj(List<IObject> e) {
    elements = e;
  }

  public ObjectType Type() {
    return IObject.ARRAY_OBJ;
  }

  public string Inspect() {
    StringBuilder sb = new();

    sb.Append("[");

    for (int i = 0; i < elements.Count; i++)
    {
      sb.Append(elements[i].Inspect());

      if (i + 1 < elements.Count) {
        sb.Append(", ");
      }
    }

    sb.Append("]");

    return sb.ToString();
  }
}

public class HashKey {
  public ObjectType type;
  public Int64 value;

  public HashKey(ObjectType t, Int64 v) {
    type = t;
    value = v;
  }
}

public class HashPair {
  public IObject key;
  public IObject value;

  public HashPair(IObject k, IObject v) {
    key = k;
    value = v;
  }
}

public class Hash : IObject {
  public Dictionary<Int64, HashPair>? pairs;

  public ObjectType Type() {
    return IObject.HASH_OBJ;
  }

  public string Inspect() {
    StringBuilder sb = new();

    sb.Append("{");

    if (pairs != null) {
      var count = 0;

      foreach (var item in pairs)
      {
        sb.Append($"{item.Value.key.Inspect()} : {item.Value.value.Inspect()}");

        ++count;        

        if (count < pairs.Count) {
          sb.Append(", ");
        }
      }
    }

    sb.Append("}");

    return sb.ToString();
  }
}

