public class Builtins {
  public Dictionary<string, Builtin> store = new();

  public Builtins() {
    store["len"] = new Builtin(delegate (IObject[] args) {
      if (args.Length == 0) {
        return new Integer(0);
      }
      else if (args.Length > 1) {
        return new Error($"wrong number of arguments. got={args.Length}, want=1");
      }

      return args[0] switch {
        StringObj s => new Integer(s.value.Length),
        _ => new Error($"unsupported argument type: {args[0].Type()}"),
      };
    });
  }
}