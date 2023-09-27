namespace hulk_interpreter;

public class Call : Expression
{
    public string identifier;
    public List<Expression> arguments;
    public Function function;

    public Call(string identifier, List<Expression> arguments, Function function)
    {
        this.function = function;
        this.identifier = identifier;
        this.arguments = arguments;
    }
}
