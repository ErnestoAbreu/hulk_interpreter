namespace hulk_interpreter;

class Binary : Expression
{
    private Expression left;
    private TokenType _operator;
    private Expression right;

    public Binary(Expression left, TokenType _operator, Expression right)
    {
        this.left = left;
        this._operator = _operator;
        this.right = right;
    }
}
