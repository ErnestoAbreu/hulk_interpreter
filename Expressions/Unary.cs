namespace hulk_interpreter;
class Unary : Expression
{
    public TokenType _operator;
    public Expression right;

    public Unary(TokenType _operator, Expression right)
    {
        this._operator = _operator;
        this.right = right;
    }
}