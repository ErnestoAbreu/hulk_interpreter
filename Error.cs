namespace hulk_interpreter;

enum ErrorType
{
    LEXICAL_ERROR,
    SYNTAX_ERROR,
    SEMANTIC_ERROR,
}

static class Error
{
    public static bool hadError = false;

    public static void Report(ErrorType type, string message)
    {
        Console.WriteLine(type + ": " + message);
        hadError = true;
    }
}
