using System.Collections;
using System.Diagnostics.Metrics;

namespace hulk_interpreter;

enum ErrorType
{
    LEXICAL_ERROR,
    SYNTAX_ERROR,
    SEMANTIC_ERROR,
}

class Error : Exception
{
    public static bool hadError = false;
    ErrorType type;
    string message;
    int numToken;
    List<Token> tokens = null!;

    public Error(ErrorType type, string message)
    {
        this.type = type;
        this.message = message;
        hadError = true;
    }

    public Error(ErrorType type, string message, int numToken, List<Token> tokens)
    {
        this.type = type;
        this.message = message;
        this.numToken = numToken;
        this.tokens = tokens;
        hadError = true;
    }

    public void Report()
    {
        switch (type)
        {
            case ErrorType.LEXICAL_ERROR:
                LexicalError();
                break;
            case ErrorType.SYNTAX_ERROR:
                SyntaxError();
                break;
            case ErrorType.SEMANTIC_ERROR:
                SemanticError();
                break;
        }
    }

    private void LexicalError()
    {
        Console.WriteLine(type + ": " + message);
    }

    private void SyntaxError()
    {
        Console.WriteLine(type + ": " + message);

        Console.Write('\t');
        int counter = 0;
        for (int i = 0; i < tokens.Count - 1; ++i)
        {
            if (i < numToken)
                counter += tokens[i].Lexeme.Length + 1;
            Console.Write(tokens[i].Lexeme + " ");
        }

        Console.WriteLine();

        Console.Write('\t');
        for (int i = 0; i < counter; ++i)
            Console.Write(" ");
        Console.WriteLine("^");
    }

    private void SemanticError()
    {
        Console.WriteLine(type + ": " + message);
    }
}
