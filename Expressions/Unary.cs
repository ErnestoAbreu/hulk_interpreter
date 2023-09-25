namespace hulk_interpreter;
class Unary : Expression
{
    private readonly TokenType _operator;
    private readonly Expression right;

    Unary(TokenType _operator, Expression right)
    {
        this._operator = _operator;
        this.right = right;
    }
}