namespace hulk_interpreter;

class Literal : Expression
{
    public object literal;

    public Literal(object literal)
    {
        this.literal = literal;
    }
}
