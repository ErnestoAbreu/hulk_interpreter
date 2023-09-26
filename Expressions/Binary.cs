namespace hulk_interpreter;

class Binary : Expression
{
    public Expression left;
    public TokenType _operator;
    public Expression right;

    public Binary(Expression left, TokenType _operator, Expression right)
    {
        this.left = left;
        this._operator = _operator;
        this.right = right;
    }
}
