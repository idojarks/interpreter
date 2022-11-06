public class Environment {
  Dictionary<string, IObject> store = new();
  Environment? outer;

  public IObject? Get(string name) {
    if (store.TryGetValue(name, out var obj)) {
      return obj;
    }

    if (outer != null) {
      return outer.Get(name);
    }

    return null;
  }

  public IObject Set(string name, IObject val) {
    store[name] = val;

    return val;
  }

  public static Environment NewEnclosedEnvironment(Environment outer) {
    var env = new Environment();
    env.outer = outer;

    return env;
  }
}