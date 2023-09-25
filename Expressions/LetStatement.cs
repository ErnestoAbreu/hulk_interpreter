namespace hulk_interpreter;

class LetStatement : Expression
{
    private readonly Expression declarationBody;
    private readonly Expression body;

    public LetStatement(Expression declarationBody, Expression body)
    {
        this.declarationBody = declarationBody;
        this.body = body;
    }
}
