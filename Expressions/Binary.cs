namespace hulk_interpreter;

class Binary : Expression
{
    public Expression left;
    public Token token;
    public Expression right;

    public Binary(Expression left, Token token, Expression right)
    {
        this.left = left;
        this.token = token;
        this.right = right;
    }
}
