
enum TokenType {
    // Single-characters
    LEFT_PAREN, RIGHT_PAREN,
    COMMA, DOT, SEMICOLON, 
    
    // Operations
    MINUS, PLUS, 
    DIVISION,   // /
    PRODUCT,    // *
    POWER,      // ^
    AT,          // @ 
    EQUAL,
    
    // Boolean operators
    AND, OR, NOT,

    // Comparitions
    EQUAL_EQUAL, NOT_EQUAL, 
    GREATER, GREATER_EQUAL,
    LESS, LESS_EQUAL,

    // Literals
    IDENTIFIER, STRING, NUMBER, TRUE, FALSE,

    // Keywords
    LET, IN, IF, ELSE, FUNCTION, INLINE_FUN, // =>

    // Functions and Constants
    PRINT, SQRT, SIN, COS, EXP, LOG, RAND, PI, EULER,

    EOF // End Of the File
}

class Token {
    public TokenType Type;
    public string Lexeme;

    Token(TokenType type, string lexeme) {
        this.Type = type;
        this.Lexeme = lexeme;
    }

    public string ToString() {
        return Lexeme;
    }
}