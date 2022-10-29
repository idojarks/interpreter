global using ObjectType = System.String;

public interface IObject {
  ObjectType Type();
  string Inspect();

  const string INTEGER_OBJ = "INTEGER";
  const string BOOLEAN_OBJ = "BOOLEAN";
  const string NULL_OBJ = "NULL";
  const string RETURN_VALUE_OBJ = "RETURN_VALUE";
  const string ERROR_OBJ = "ERROR";
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

public class Bool : IObject {
  public bool _value;

  public Bool(bool v) {
    _value = v;
  }

  public string Inspect() {
    return $"{_value.ToString()}";
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