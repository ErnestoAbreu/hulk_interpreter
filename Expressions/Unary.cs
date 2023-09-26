namespace hulk_interpreter;
class Unary : Expression
{
    public Token token;
    public Expression right;

    public Unary(Token token, Expression right)
    {
        this.token = token;
        this.right = right;
    }
}