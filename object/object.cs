global using ObjectType = System.String;

 public interface IObject {
  ObjectType Type();
  string Inspect();

  const string INTEGER_OBJ = "INTEGER";
  const string BOOLEAN_OBJ = "BOOLEAN";
  const string NULL_OBJ = "NULL";
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
