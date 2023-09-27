namespace hulk_interpreter;

class Assing : Expression
{
    public string identifier;
    public Expression value;

    public Assing(string identifier, Expression value)
    {
        this.identifier = identifier;
        this.value = value;
    }
}
