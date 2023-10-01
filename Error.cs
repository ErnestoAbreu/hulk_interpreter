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

    public static void Report(ErrorType type, string message, int numToken = -1)
    {
        if(numToken != -1)
            Console.WriteLine(type + ": " + message + "in token number " + numToken);
        else 
            Console.WriteLine(type + ": " + message);
        hadError = true;
    }
}
