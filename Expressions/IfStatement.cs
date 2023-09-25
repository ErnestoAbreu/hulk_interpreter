namespace hulk_interpreter;

class IfStatement : Expression
{
    private readonly Expression ifBody;
    private readonly Expression elseBody;

    public IfStatement(Expression ifBody, Expression elseBody)
    {
        this.ifBody = ifBody;
        this.elseBody = elseBody;
    }

}