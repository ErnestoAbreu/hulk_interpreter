namespace hulk_interpreter;

public class Evaluate
{
    private Expression ast;

    public Evaluate(Expression ast)
    {
        this.ast = ast;
    }

    public object Run()
    {
        try
        {
            return GetValue(ast, new Dictionary<string, object>());
        }
        catch (Error error)
        {
            error.Report();
            return null!;
        }
    }

    public static object GetValue(Expression expr, Dictionary<string, object> value)
    {
        Program.count++;
        if (Program.count > Program.stack_limit)
            throw new StackOverflowException();

        switch (expr)
        {
            case Literal:
                Literal literal = (Literal)expr;
                return literal.value;

            case Variable:
                Variable variable = (Variable)expr;
                if (!value.ContainsKey(variable.name))
                    throw new Error(
                        ErrorType.SEMANTIC_ERROR,
                        "Name " + variable.name + " is no defined."
                    );
                return value[variable.name];

            case Binary:
                Binary binary = (Binary)expr;
                return binary.Calculate(
                    GetValue(binary.left, value),
                    GetValue(binary.right, value)
                );

            case Unary:
                Unary unary = (Unary)expr;
                return unary.Calculate(GetValue(unary.right, value));

            case Call:
                Call call = (Call)expr;
                return call.Calculate(value);

            case Function:
                return "Function has been declarated correctly";

            case IfStatement:
                IfStatement ifElse = (IfStatement)expr;
                return ifElse.Calculate(value);

            case LetStatement:
                LetStatement letStatement = (LetStatement)expr;
                return letStatement.Calculate(value);

            default:
                return null!;
        }
    }
}
