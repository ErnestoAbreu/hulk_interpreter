namespace hulk_interpreter;

public class Lexer
{
    private string code = "";
    private int start = 0;
    private int current = 0;
    private bool Scanned = false;
    private List<Token> Tokens = new List<Token>();
    private Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>();

    public Lexer(string code)
    {
        this.code = code + "#";
        keywords["true"] = TokenType.TRUE;
        keywords["false"] = TokenType.FALSE;
        keywords["let"] = TokenType.LET;
        keywords["in"] = TokenType.IN;
        keywords["if"] = TokenType.IF;
        keywords["else"] = TokenType.ELSE;
        keywords["function"] = TokenType.FUNCTION;
        keywords["print"] = TokenType.PRINT;
        keywords["sqrt"] = TokenType.SQRT;
        keywords["sin"] = TokenType.SIN;
        keywords["cos"] = TokenType.COS;
        keywords["exp"] = TokenType.EXP;
        keywords["log"] = TokenType.LOG;
        keywords["rand"] = TokenType.RAND;
        keywords["PI"] = TokenType.PI;
        keywords["E"] = TokenType.EULER;
    }

    private char GetNext()
    {
        return code[current + 1];
    }

    private bool Advance()
    {
        current++;
        if (current == code.Length)
            return false;

        return true;
    }

    private string GetSubstr()
    {
        return code.Substring(start, current - start + 1);
    }

    private Token Match(char character)
    {
        Token token = new Token(TokenType.EOF, "");

        if (character == '=')
        {
            switch (code[current + 1])
            {
                case '=':
                    token = new Token(TokenType.EQUAL_EQUAL, "==");
                    current++;
                    break;
                case '>':
                    current++;
                    token = new Token(TokenType.INLINE_FUN, "=>");
                    break;
                default:
                    token = new Token(TokenType.EQUAL, "=");
                    break;
            }
        }
        if (character == '!')
        {
            if (code[current + 1] == '=')
            {
                current++;
                token = new Token(TokenType.NOT_EQUAL, "!=");
            }
            else
                token = new Token(TokenType.NOT, "!");
        }
        if (character == '>')
        {
            if (code[current + 1] == '=')
            {
                current++;
                token = new Token(TokenType.GREATER_EQUAL, ">=");
            }
            else
                token = new Token(TokenType.GREATER, ">");
        }
        if (character == '<')
        {
            if (code[current + 1] == '=')
            {
                current++;
                token = new Token(TokenType.LESS_EQUAL, "<=");
            }
            else
                token = new Token(TokenType.LESS, "<");
        }

        return token;
    }

    private Token StringMatch()
    {
        Token token = new Token(TokenType.EOF, "");

        current++;
        while (code[current] != '"' && current < code.Length)
            current++;

        if (current == code.Length)
            Error.Report(ErrorType.LEXICAL_ERROR, GetSubstr());
        else
            token = new Token(TokenType.STRING, GetSubstr(), GetSubstr());

        return token;
    }

    private Token NumberMatch()
    {
        Token token = new Token(TokenType.EOF, "");

        bool error = false;
        while (Char.IsDigit(GetNext()) || Char.IsLetter(GetNext()) || GetNext() == '.')
        {
            if (Char.IsLetter(GetNext()))
                error = true;

            Advance();
        }
        if (error || code[current] == '.')
            Error.Report(ErrorType.LEXICAL_ERROR, GetSubstr());
        else
            token = new Token(TokenType.NUMBER, GetSubstr(), double.Parse(GetSubstr()));

        return token;
    }

    private Token IdentifierMatch()
    {
        Token token = new Token(TokenType.EOF, "");

        while (Char.IsDigit(GetNext()) || Char.IsLetter(GetNext()) || GetNext() == '_')
        {
            Advance();
        }

        foreach (string key in keywords.Keys)
        {
            if (GetSubstr() == key)
                return new Token(keywords[key], GetSubstr());
        }

        return new Token(TokenType.IDENTIFIER, GetSubstr());
    }

    private Token NextToken()
    {
        Token token = new Token(TokenType.EOF, "error");

        while (code[current] == ' ')
            start = ++current;

        switch (code[current])
        {
            case '(':
                token = new Token(TokenType.LEFT_PAREN, "(");
                break;
            case ')':
                token = new Token(TokenType.RIGHT_PAREN, ")");
                break;
            case ',':
                token = new Token(TokenType.COMMA, ",");
                break;
            case ';':
                token = new Token(TokenType.SEMICOLON, ";");
                break;
            case '-':
                token = new Token(TokenType.MINUS, "-");
                break;
            case '+':
                token = new Token(TokenType.PLUS, "+");
                break;
            case '/':
                token = new Token(TokenType.DIVISION, "/");
                break;
            case '*':
                token = new Token(TokenType.PRODUCT, "*");
                break;
            case '^':
                token = new Token(TokenType.POWER, "^");
                break;
            case '@':
                token = new Token(TokenType.AT, "@");
                break;
            case '&':
                token = new Token(TokenType.AND, "&");
                break;
            case '|':
                token = new Token(TokenType.OR, "|");
                break;
            case '=':
                token = Match('=');
                break;
            case '!':
                token = Match('!');
                break;
            case '>':
                token = Match('>');
                break;
            case '<':
                token = Match('<');
                break;
            case '"':
                token = StringMatch();
                break;
        }

        if (Char.IsDigit(code[current]))
            token = NumberMatch();

        if (Char.IsLetter(code[current]))
            token = IdentifierMatch();

        if (code[current] == '#')
            token.Lexeme = "#";

        if (token.Lexeme == "error")
            Error.Report(ErrorType.LEXICAL_ERROR, code.Substring(start, 1));

        start = ++current;

        return token;
    }

    public List<Token> GetTokens()
    {
        do
        {
            Tokens.Add(NextToken());

            if (Tokens.Last().Type == TokenType.EOF)
                Scanned = true;
        } while (!Scanned);

        return Tokens;
    }

    public override string ToString()
    {
        string tokens = "";

        foreach (Token token in Tokens)
            tokens += token.ToString();

        return tokens;
    }
}
