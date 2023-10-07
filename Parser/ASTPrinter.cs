using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace hulk_interpreter;

public static class ASTPriter
{
    public static string Print(Expression expr)
    {
        if (expr is Binary)
        {
            Binary binary = (Binary)expr;
            return "[ "
                + Print(binary.left)
                + " ] "
                + binary.token
                + " [ "
                + Print(binary.right)
                + " ]";
        }

        if (expr is Unary)
        {
            Unary unary = (Unary)expr;

            return "[ " + unary.token + " [ " + Print(unary.right) + " ]";
        }

        if (expr is Literal)
        {
            Literal literal = (Literal)expr;
            return literal.value.ToString()!;
        }

        if (expr is IfStatement)
        {
            IfStatement ifElse = (IfStatement)expr;
            return "if ( "
                + Print(ifElse.condition)
                + " ) \n"
                + Print(ifElse.ifBody)
                + "\nelse\n"
                + Print(ifElse.elseBody);
        }

        if (expr is Assing)
        {
            Assing assing = (Assing)expr;
            return Print(assing.value);
        }

        if (expr is Variable)
        {
            Variable variable = (Variable)expr;
            return variable.name;
        }

        if (expr is LetStatement)
        {
            LetStatement letStatement = (LetStatement)expr;

            string str = "let ";

            foreach (Assing assing in letStatement.assingBody)
                str += assing.identifier + " = " + Print(assing) + ", ";

            str += " in ";

            return str + Print(letStatement.body);
        }

        if (expr is Call)
        {
            Call call = (Call)expr;

            string str = call.identifier + "( ";

            foreach (Expression arg in call.arguments)
                str += Print(arg) + ", ";

            return str + " )";
        }

        if (expr is Function)
        {
            return "Function has been declarated correctly";
        }

        return "Unexpected error";
    }
}
