namespace hulk_interpreter;

public class Call : Expression
{
    public string identifier;
    public List<Expression> arguments;
    public Function function;

    public Call(string identifier, List<Expression> arguments, Function function)
    {
        this.function = function;
        this.identifier = identifier;
        this.arguments = arguments;
    }

    public object Calculate(Dictionary<string, object> value)
    {
        List<object> values = new List<object>();
        foreach (Expression expr in arguments)
        {
            Evaluate param = new Evaluate(expr, value);
            values.Add(param.Run());
        }

        switch (identifier)
        {
            case "print":
                return Print(values);
            case "sqrt":
                return Sqrt(values);
            case "sin":
                return Sin(values);
            case "cos":
                return Cos(values);
            case "exp":
                return Exp(values);
            case "log":
                return Log(values);
            case "rand":
                return Rand(values);
            default:
                return Fun(values);
        }
    }

    private object Fun(List<object> args)
    {
        if (args.Count == function.arguments.Count)
        {
            int counter = 0;
            foreach (string name in function.arguments)
            {
                function.value[name] = args[counter++];
            }

            Evaluate body = new Evaluate(function.body, function.value);
            return body.Run();
        }

        throw new Exception(
            "Function "
                + identifier
                + " receives "
                + function.arguments.Count
                + " argument(s), but "
                + args.Count
                + " were given"
        );
    }

    private static object Print(List<object> args)
    {
        if (args.Count != 1)
            throw new Exception(
                "Function 'print' receives 1 argument(s), but " + args.Count + " were given"
            );

        return args[0];
    }

    private static object Sqrt(List<object> args)
    {
        if (args.Count != 1)
            throw new Exception(
                "Function 'sqrt' receives 1 argument(s), but " + args.Count + " were given"
            );
        if (args[0] is double arg)
            return Math.Sqrt(arg);

        throw new Exception("Function 'sqrt' must receive a number");
    }

    private static object Sin(List<object> args)
    {
        if (args.Count != 1)
            throw new Exception(
                "Function 'sin' receives 1 argument(s), but " + args.Count + " were given"
            );
        if (args[0] is double arg)
            return Math.Sin(arg);

        throw new Exception("Function 'sin' must receive a number");
    }

    private static object Cos(List<object> args)
    {
        if (args.Count != 1)
            throw new Exception(
                "Function 'cos' receives 1 argument(s), but " + args.Count + " were given"
            );
        if (args[0] is double arg)
            return Math.Cos(arg);

        throw new Exception("Function 'cos' must receive a number");
    }

    private static object Exp(List<object> args)
    {
        if (args.Count != 1)
            throw new Exception(
                "Function 'exp' receives 1 argument(s), but " + args.Count + " were given"
            );
        if (args[0] is double arg)
            return Math.Exp(arg);

        throw new Exception("Function 'exp' must receive a number");
    }

    private static object Log(List<object> args)
    {
        if (args.Count != 2)
            throw new Exception(
                "Function 'log' receives 2 argument(s), but " + args.Count + " were given"
            );
        if (args[0] is double arg0 && args[1] is double arg1)
            return Math.Log(arg1, arg0);

        throw new Exception("Function 'log' must receives numbers");
    }

    private static object Rand(List<object> args)
    {
        if (args.Count != 0)
            throw new Exception("Function 'rand' cannot receive arguments");

        Random random = new Random();
        return random.NextDouble();
    }
}
