﻿using System.Data;

namespace hulk_interpreter;

class Program
{
    static void Main(string[] args)
    {
        Functions.Init();
        Console.WriteLine("[Havana University Language for Kompilers - Interpreter]");

        while (true)
        {
            Console.Write(">");

            string? code = Console.ReadLine();

            if (code == null)
                break;

            Error.hadError = false;
            Run(code);
        }
    }

    static void Run(string code)
    {
        Lexer lexer = new Lexer(code);
        List<Token> tokens = lexer.GetTokens();

        if (Error.hadError)
            return;

        // foreach (Token token in tokens)
        // {
        //     Console.WriteLine(token);
        // }

        Parser parser = new Parser(tokens);
        Expression ast = parser.Parse();

        if (Error.hadError)
            return;

        // Console.WriteLine(ASTPriter.Print(ast));

        Evaluate evaluate = new Evaluate(ast);
        object Output = evaluate.Run();
        if(Output != null)
            Console.WriteLine(Output);
    }
}
