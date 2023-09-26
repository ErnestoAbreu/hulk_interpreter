namespace hulk_interpreter;

public static class ASTPriter
{
    public static string Print(Expression expr)
    {
        if (expr is Binary)
        {
            Binary binary = (Binary)expr;
            return "[ " + Print(binary.left) + " ] " + binary.token + " [ " + Print(binary.right) + " ]";
        }

        if (expr is Unary)
        {
            Unary unary = (Unary)expr;

            return "[ " + unary.token + " [ " + Print(unary.right) + " ]";
        }

        if(expr is Literal)
        {
            Literal literal = (Literal)expr;
            return literal.literal.ToString()!;
        }

        if(expr is IfStatement)
        {
            IfStatement ifElse = (IfStatement)expr;
            return "if ( " + Print(ifElse.condition) + " ) \n" + "    " + Print(ifElse.ifBody) + "\nelse\n" + Print(ifElse.elseBody);
        }

        return "ERROR";
    }
}
