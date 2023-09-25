namespace hulk_interpreter;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write(">");

            string? code = Console.ReadLine();

            if (code == null)
                break;

            Run(code);
        }
    }

    static void Run(string code)
    {
        Lexer lexer = new Lexer(code);
        List<Token> tokens = lexer.GetTokens();

        // foreach (Token token in tokens)
        // {
        //     Console.WriteLine(token);
        // }

        if (Error.hadError)
            return;

        Parser parser = new Parser(tokens);
        Expression Expr = parser.Parse();

        if (Error.hadError)
            return;
    }
}
