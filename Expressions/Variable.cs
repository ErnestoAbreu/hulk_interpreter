namespace hulk_interpreter;

public class Variable : Expression
{
    public string name;
    public object value = null!;

    public Variable(string name)
    {
        this.name = name;
    }
}
