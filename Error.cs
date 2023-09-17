namespace hulk_interpreter;

enum ErrorType
{
    LEXICAL_ERROR,
    SYNTAX_ERROR,
    SEMANTIC_ERROR,
}

static class Error
{
    static bool hadError = false;

    static void Report(ErrorType type, string message)
    {
        Console.WriteLine(type + ": " + message);
        hadError = true;
    }
}
