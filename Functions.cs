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
}
