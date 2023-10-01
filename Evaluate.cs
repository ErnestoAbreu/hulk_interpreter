using System.Linq.Expressions;

namespace hulk_interpreter;

public class Evaluate
{
    public Expression ast;

    public Evaluate(Expression ast)
    {
        this.ast = ast;
    }

    public object Run()
    {
        try
        {
            return GetValue(ast);
        }
        catch (Exception e)
        {
            Error.Report(ErrorType.SEMANTIC_ERROR, e.Message);
            return null!;
        }
    }

    public object GetValue(Expression expr)
    {
        switch (expr)
        {
            case Literal:
            {
                Literal literal = (Literal)expr;
                return literal.literal;
            }

            case Binary:
            {
                Binary binary = (Binary)expr;
                return binary.Calculate(GetValue(binary.left), GetValue(binary.right));
            }

            case Unary:
            {
                Unary unary = (Unary)expr;
                return unary.Calculate(GetValue(unary.right));
            }

            default:
                return null!;
        }

        // if (expr is IfStatement)
        // {
        //     IfStatement ifElse = (IfStatement)expr;
        //     return "if ( "
        //         + Print(ifElse.condition)
        //         + " ) \n"
        //         + Print(ifElse.ifBody)
        //         + "\nelse\n"
        //         + Print(ifElse.elseBody);
        // }

        // if (expr is Assing)
        // {
        //     Assing assing = (Assing)expr;
        //     return Print(assing.value);
        // }

        // if (expr is Variable)
        // {
        //     Variable variable = (Variable)expr;
        //     return variable.name;
        // }

        // if (expr is LetStatement)
        // {
        //     LetStatement letStatement = (LetStatement)expr;

        //     string str = "let ";

        //     foreach (Assing assing in letStatement.assingBody)
        //         str += assing.identifier + " = " + Print(assing) + ", ";

        //     str += " in ";

        //     return str + Print(letStatement.body);
        // }

        // if (expr is Call)
        // {
        //     Call call = (Call)expr;

        //     string str = call.identifier + "( ";

        //     foreach (Expression arg in call.arguments)
        //         str += Print(arg) + ", ";

        //     return str + " )";
        // }

        // if (expr is Function)
        // {
        //     return "Function has been declarated correctly";
        // }

        // return "Unexpected error";
    }
}
