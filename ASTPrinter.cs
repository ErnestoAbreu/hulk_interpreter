namespace hulk_interpreter;

public static class ASTPriter
{
    public static string Print(Expression expr)
    {
        if (expr is Binary)
        {
            Binary binary = (Binary)expr;
            return "[ " + Print(binary.left) + " ] " + binary._operator + " [ " + Print(binary.right) + " ]";
        }

        if (expr is Unary)
        {
            Unary unary = (Unary)expr;

            return "[ " + unary._operator + " [ " + Print(unary.right) + " ]";
        }

        if(expr is Literal)
        {
            Literal literal = (Literal)expr;
            return literal.literal.ToString()!;
        }

        return "ERROR";
    }
}
