namespace hulk_interpreter;

class IfStatement : Expression
{
    public Expression condition;
    public Expression ifBody;
    public Expression elseBody;

    public IfStatement(Expression condition, Expression ifBody, Expression elseBody)
    {
        this.condition = condition;
        this.ifBody = ifBody;
        this.elseBody = elseBody;
    }

}