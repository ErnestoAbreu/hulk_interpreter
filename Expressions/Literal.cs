namespace hulk_interpreter;

class Literal : Expression
{
    private readonly TokenType token;
    private readonly object literal;

    public Literal(TokenType token, object literal)
    {
        this.token = token;
        this.literal = literal;
    }
}
