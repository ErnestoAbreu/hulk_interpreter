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
        if (Evaluate.GetValue(condition, value) is bool check)
        {
            if (check)
            {
                return Evaluate.GetValue(ifBody, value);
            }
            else
            {
                return Evaluate.GetValue(elseBody, value);
            }
        }

        throw new Error(ErrorType.SEMANTIC_ERROR, "If condition must return a boolean value.");
    }
}
