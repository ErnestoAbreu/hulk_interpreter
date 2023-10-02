using Microsoft.Win32;

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

    public object Calculate(Dictionary<string, object> value)
    {
        Evaluate evaluateCondition = new Evaluate(condition, value);
        if (evaluateCondition.Run() is bool check)
        {
            if (check)
            {
                Evaluate evaluate = new Evaluate(ifBody, value);
                return evaluate.Run();
            }
            else { 
                Evaluate evaluate = new Evaluate(elseBody, value);
                return evaluate.Run();
            }
        }

        throw new Exception("If condition must be a bool.");
    }
}
