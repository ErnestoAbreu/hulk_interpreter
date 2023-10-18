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

        object returnValue = null!;

        switch (expr)
        {
            case Literal:
                Literal literal = (Literal)expr;
                returnValue = literal.value;
                break;

            case Variable:
                Variable variable = (Variable)expr;
                if (!value.ContainsKey(variable.name))
                    throw new Error(
                        ErrorType.SEMANTIC_ERROR,
                        "Name " + variable.name + " is no defined."
                    );
                returnValue = value[variable.name];
                break;

            case Binary:
                Binary binary = (Binary)expr;
                returnValue = binary.Calculate(
                    GetValue(binary.left, value),
                    GetValue(binary.right, value)
                );
                break;

            case Unary:
                Unary unary = (Unary)expr;
                returnValue = unary.Calculate(GetValue(unary.right, value));
                break;

            case Call:
                Call call = (Call)expr;
                returnValue = call.Calculate(value);
                break;

            case Function:
                return "Function has been declarated correctly";

            case IfStatement:
                IfStatement ifElse = (IfStatement)expr;
                returnValue = ifElse.Calculate(value);
                break;

            case LetStatement:
                LetStatement letStatement = (LetStatement)expr;
                returnValue = letStatement.Calculate(value);
                break;

            default:
                return null!;
        }

        Program.count--;
        return returnValue;
    }
}
