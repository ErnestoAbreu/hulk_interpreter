namespace hulk_interpreter;

class Binary : Expression
{
    public Expression left;
    public Token token;
    public Expression right;

    public Binary(Expression left, Token token, Expression right)
    {
        this.left = left;
        this.token = token;
        this.right = right;
    }

    public object Calculate(object valueLeft, object valueRight)
    {
        switch (token.Type)
        {
            case TokenType.PLUS:
                return Sum(valueLeft, valueRight);
            case TokenType.MINUS:
                return Rest(valueLeft, valueRight);
            case TokenType.PRODUCT:
                return Product(valueLeft, valueRight);
            case TokenType.DIVISION:
                return Div(valueLeft, valueRight);
            case TokenType.MOD:
                return Mod(valueLeft, valueRight);
            case TokenType.POWER:
                return Power(valueLeft, valueRight);
            case TokenType.AT:
                return Concat(valueLeft, valueRight);
            case TokenType.AND:
                return And(valueLeft, valueRight);
            case TokenType.OR:
                return Or(valueLeft, valueRight);
            case TokenType.EQUAL_EQUAL:
                return Equal_Equal(valueLeft, valueRight);
            case TokenType.NOT_EQUAL:
                return Not_Equal(valueLeft, valueRight);
            case TokenType.LESS_EQUAL:
                return Less_Equal(valueLeft, valueRight);
            case TokenType.GREATER_EQUAL:
                return Greater_Equal(valueLeft, valueRight);
            case TokenType.GREATER:
                return Greater(valueLeft, valueRight);
            case TokenType.LESS:
                return Less(valueLeft, valueRight);
        }

        return null!;
    }

    private static object Sum(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
            return (double)valueLeft + (double)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator + cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Rest(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
            return (double)valueLeft - (double)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator - cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Product(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
            return (double)valueLeft * (double)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator * cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Div(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
        {
            if ((double)valueRight == 0)
                throw new Error(ErrorType.SEMANTIC_ERROR, "Division by zero");

            return (double)valueLeft / (double)valueRight;
        }

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator / cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Mod(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
        {
            if ((double)valueRight == 0)
                throw new Error(ErrorType.SEMANTIC_ERROR, "Modulo by zero");

            return (double)valueLeft % (double)valueRight;
        }

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator % cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Power(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
            return Math.Pow((double)valueLeft, (double)valueRight);

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator ^ cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object And(object valueLeft, object valueRight)
    {
        if (valueLeft is bool && valueRight is bool)
            return (bool)valueLeft && (bool)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator & cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Or(object valueLeft, object valueRight)
    {
        if (valueLeft is bool && valueRight is bool)
            return (bool)valueLeft || (bool)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator | cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Less(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
            return (double)valueLeft < (double)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator < cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Greater(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
            return (double)valueLeft > (double)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator > cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Less_Equal(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
            return (double)valueLeft <= (double)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator <= cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Greater_Equal(object valueLeft, object valueRight)
    {
        if (valueLeft is double && valueRight is double)
            return (double)valueLeft >= (double)valueRight;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator >= cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Equal_Equal(object valueLeft, object valueRight)
    {
        // if (valueLeft is double && valueRight is double)
        // return (double)valueLeft == (double)valueRight;
        return valueLeft.Equals(valueRight);

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator == cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Not_Equal(object valueLeft, object valueRight)
    {
        // if (valueLeft is double && valueRight is double)
        // return (double)valueLeft == (double)valueRight;
        return !valueLeft.Equals(valueRight);

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator == cannot be used between " + valueLeft + " and " + valueRight
        );
    }

    private static object Concat(object valueLeft, object valueRight)
    {
        string str = "";
        switch (valueLeft)
        {
            case string:
                str += valueLeft;
                break;
            case double:
                str += (double)valueLeft;
                break;
            case bool:
                str += (bool)valueLeft;
                break;
        }
        switch (valueRight)
        {
            case string:
                str += valueRight;
                break;
            case double:
                str += (double)valueRight;
                break;
            case bool:
                str += (bool)valueRight;
                break;
        }
        return str;

        throw new Error(
            ErrorType.SEMANTIC_ERROR,
            "Operator @ cannot be used between " + valueLeft + " and " + valueRight
        );
    }
}
