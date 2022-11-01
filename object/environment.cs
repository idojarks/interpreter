public class Environment {
  Dictionary<string, IObject> store = new();

  public IObject? Get(string name) {
    if (store.TryGetValue(name, out var obj)) {
      return obj;
    }

    return null;
  }

  public IObject Set(string name, IObject val) {
    store[name] = val;

    return val;
  }
}