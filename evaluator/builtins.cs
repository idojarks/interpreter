public class Builtins {
  public Dictionary<string, Builtin> store = new();

  public Builtins() {
    store["len"] = new Builtin(delegate (IObject[] args) {
      if (args.Length == 0) {
        return new Integer(0);
      }
      else if (args.Length > 1) {
        return new Error($"Builtins.len(): wrong number of arguments. got={args.Length}, want=1");
      }

      return args[0] switch {
        StringObj s => new Integer(s.value.Length),
        ArrayObj ar => new Integer(ar.elements.Count),
        _ => new Error($"Builtins.len(): unsupported argument type={args[0].Type()}"),
      };
    });
    store["first"] = new Builtin(delegate (IObject[] args) {
      if (args.Length != 1) {
        return new Error($"Builtins.first(): wrong number of arguments. got={args.Length}, want=1");
      }

      if (args[0] is ArrayObj ar) {
        if (ar.elements.Count == 0) {
          return Objects.nullObj;
        }
        
        return ar.elements[0];
      }
      else {
        return new Error($"Builtins.first(): unsupported argument type={args[0].Type()}");
      }
    });
    store["last"] = new Builtin(delegate (IObject[] args) {
      if (args.Length != 1) {
        return new Error($"Builtins.first(): wrong number of arguments. got={args.Length}, want=1");
      }

      if (args[0] is ArrayObj ar) {
        if (ar.elements.Count == 0) {
          return Objects.nullObj;
        }
        
        return ar.elements[ar.elements.Count-1];
      }
      else {
        return new Error($"Builtins.first(): unsupported argument type={args[0].Type()}");
      }
    });
  }
}