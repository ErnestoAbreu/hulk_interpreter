namespace hulk_interpreter;

class LetStatement : Expression
{
    public List<Assing> assingBody;
    public Expression body;
    public Dictionary<string, object> variables = new Dictionary<string, object>();

    public LetStatement(List<Assing> assingBody, Expression body)
    {
        this.assingBody = assingBody;
        this.body = body;
    }
}
