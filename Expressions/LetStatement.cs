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
            variable[assing.identifier] = Evaluate.GetValue(assing.value, variable);
        }

        return Evaluate.GetValue(body, variable);
    }
}
