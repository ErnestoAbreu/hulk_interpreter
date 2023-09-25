namespace hulk_interpreter;

class Literal : Expression
{
    private readonly object literal;

    public Literal( object literal)
    {
        this.literal = literal;
    }
}
