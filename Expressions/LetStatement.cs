using System.Globalization;

namespace hulk_interpreter;

class LetStatement : Expression
{
    public List<Assing> assingBody;
    public Expression body;

    public LetStatement(List<Assing> assingBody, Expression body)
    {
        this.assingBody = assingBody;
        this.body = body;
    }

    public object Calculate(Dictionary<string, object> value)
    {
        Dictionary<string, object> variable = new Dictionary<string, object>();

        if (value != null)
            foreach (string key in value.Keys)
            {
                variable[key] = value[key];
            }

        foreach (Assing assing in assingBody)
        {
            Evaluate evaluate = new Evaluate(assing.value, variable);
            variable[assing.identifier] = evaluate.Run();
        }

        Evaluate evaluateBody = new Evaluate(body, variable);
        return evaluateBody.Run();
    }
}
