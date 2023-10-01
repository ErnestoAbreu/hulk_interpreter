using Microsoft.Win32.SafeHandles;

namespace hulk_interpreter;

class Unary : Expression
{
    public Token token;
    public Expression right;

    public Unary(Token token, Expression right)
    {
        this.token = token;
        this.right = right;
    }

    public object Calculate(object valueRight)
    {
        switch (token.Type)
        {
            case TokenType.MINUS:
                return Minus(valueRight);
            case TokenType.NOT:
                return Not(valueRight);
        }
        return null!;
    }

    public object Minus(object valueRight)
    {
        if (valueRight is double)
            return (double)valueRight * -1;

        throw new Exception("Operator - cannot be used before " + valueRight);
    }

    public object Not(object valueRight)
    {
        if(valueRight is bool)
            return !(bool)valueRight;
        
        throw new Exception("Operator ! cannot be used before " + valueRight);
    }
}
