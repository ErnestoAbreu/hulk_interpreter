namespace hulk_interpreter;

static class Functions
{
    public static Dictionary<string, Function> functions = new Dictionary<string, Function>();

    public static bool Contains(string name)
    {
        return functions.ContainsKey(name);
    }

    public static Function Get(string name)
    {
        return functions[name];
    }

    public static void Add(string name, Function function = null!)
    {
        functions[name] = function;
    }

    public static void Init()
    {
        Add("print");
        Add("sqrt");
        Add("sin");
        Add("cos");
        Add("exp");
        Add("log");
        Add("rand");
    }
}
