namespace hulk_interpreter;

public class Function : Expression
{
    public string identifier;
    public List<string> arguments;
    public Dictionary<string, object> value = new Dictionary<string, object>();
    public Expression body;

    public Function(string identifier, List<string> arguments, Expression body)
    {
        this.identifier = identifier;
        this.arguments = arguments;
        this.body = body;
    }

    public Function Copy()
    {
        Function newFunction = new Function(identifier, arguments, body);
        if (value != null)
            foreach (string key in value.Keys)
            {
                newFunction.value[key] = value[key];
            }
        return newFunction;
    }
}
