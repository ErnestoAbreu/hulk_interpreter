namespace hulk_interpreter;

class Grouping : Expression
{
    public Expression expr;

    Grouping(Expression expr)
    {
        this.expr = expr;
    }
}