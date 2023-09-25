namespace hulk_interpreter;

class Grouping : Expression
{
    private Expression expr;

    Grouping(Expression expr)
    {
        this.expr = expr;
    }
}