namespace hulk_interpreter;

public enum TokenType
{
    // Single-characters
    LEFT_PAREN,
    RIGHT_PAREN,
    COMMA,
    SEMICOLON,

    // Operations
    MINUS,
    PLUS,
    DIVISION,
    PRODUCT,
    POWER,
    AT,
    EQUAL,

    // Boolean operators
    AND,
    OR,
    NOT,

    // Comparitions
    EQUAL_EQUAL,
    NOT_EQUAL,
    GREATER,
    GREATER_EQUAL,
    LESS,
    LESS_EQUAL,

    // Literals
    IDENTIFIER,
    STRING,
    NUMBER,
    TRUE,
    FALSE,

    // Keywords
    LET,
    IN,
    IF,
    ELSE,
    FUNCTION,
    INLINE_FUN,

    //Constants
    PI,
    EULER,

    // End Of the File
    EOF
}

public class Token
{
    public TokenType Type;
    public string Lexeme;
    public object literal;

    public Token(TokenType type, string lexeme, object literal = null!)
    {
        this.Type = type;
        this.Lexeme = lexeme;
        this.literal = literal;
    }

    public override string ToString()
    {
        return Lexeme;
    }
}
