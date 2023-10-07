using System.Security.Cryptography;

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
        keywords["let"] = TokenType.LET;
        keywords["in"] = TokenType.IN;
        keywords["if"] = TokenType.IF;
        keywords["else"] = TokenType.ELSE;
        keywords["function"] = TokenType.FUNCTION;
    }

    /* Devuelve una lista con los tokens */
    public List<Token> GetTokens()
    {
        while (!Scanned)
        {
            Tokens.Add(NextToken());

            if (Tokens.Last().Type == TokenType.EOF)
                Scanned = true;
        }

        return Tokens;
    }

    public override string ToString()
    {
        string tokens = "";

        foreach (Token token in Tokens)
            tokens += token;

        return tokens;
    }

    /* Obtiene el siguiente caracter */
    private char GetNext()
    {
        return code[current + 1];
    }

    /* Obtiene el caracter actual */
    private char GetCurrent()
    {
        return code[current];
    }

    /* Avanza al siguiente caracter, devuelve el nuevo indice */
    private int Advance()
    {
        current++;
        return current;
    }

    /* Devuelve la subcadena que esta siendo evaluada */
    private string GetSubstr()
    {
        return code.Substring(start, current - start + 1);
    }

    /* Devuelve verdadero si esta en el final */
    private bool isAtEnd()
    {
        return GetCurrent() == '#';
    }

    /* Verifica operadores de dos caracteres */
    private Token Match(char character)
    {
        Token token = new Token(TokenType.EOF, "");

        switch (character)
        {
            case '=':
                switch (GetNext())
                {
                    case '=':
                        token = new Token(TokenType.EQUAL_EQUAL, "==");
                        Advance();
                        break;
                    case '>':
                        token = new Token(TokenType.INLINE_FUN, "=>");
                        Advance();
                        break;
                    default:
                        token = new Token(TokenType.EQUAL, "=");
                        break;
                }
                break;

            case '!':
                if (GetNext() == '=')
                {
                    token = new Token(TokenType.NOT_EQUAL, "!=");
                    Advance();
                }
                else
                    token = new Token(TokenType.NOT, "!");
                break;

            case '>':
                if (GetNext() == '=')
                {
                    token = new Token(TokenType.GREATER_EQUAL, ">=");
                    Advance();
                }
                else
                    token = new Token(TokenType.GREATER, ">");
                break;
            case '<':
                if (GetNext() == '=')
                {
                    token = new Token(TokenType.LESS_EQUAL, "<=");
                    Advance();
                }
                else
                    token = new Token(TokenType.LESS, "<");
                break;
        }

        return token;
    }

    /* Procesa las cadenas de caracteres */
    private Token StringMatch()
    {
        Token token = new Token(TokenType.EOF, "");

        // if (GetNext() == '\"')
        // {
        //     Advance();
        //     return new Token(TokenType.STRING, "", "");
        // }

        start++;

        while (!isAtEnd() && GetNext() != '"')
        {
            if (GetNext() == '\\')
                Advance();

            Advance();
        }

        if (isAtEnd())
        {
            start--;
            current--;
            Error.Report(ErrorType.LEXICAL_ERROR, GetSubstr() + " expect \" character");
        }
        else
        {
            string literal = GetSubstr();
            literal = literal.Replace("\\\"", "\"");
            literal = literal.Replace("\\t", "\t");
            literal = literal.Replace("\\n", "\n");
            literal = literal.Replace("\\\\", "\\");
            token = new Token(TokenType.STRING, GetSubstr(), literal);
        }

        Advance();

        return token;
    }

    /* Procesa los numeros */
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

        if (error || GetCurrent() == '.')
            Error.Report(ErrorType.LEXICAL_ERROR, "'" + GetSubstr() + "' is not a valid token.");
        else
            token = new Token(
                TokenType.NUMBER,
                GetSubstr(),
                double.Parse(GetSubstr().Replace('.', ','))
            );

        return token;
    }

    /* Procesa los nombres de variables o funciones */
    private Token IdentifierMatch()
    {
        Token token = new Token(TokenType.EOF, "");

        while (Char.IsDigit(GetNext()) || Char.IsLetter(GetNext()) || GetNext() == '_')
        {
            Advance();
        }

        if (GetSubstr() == "true")
            return new Token(TokenType.TRUE, GetSubstr(), true);

        if (GetSubstr() == "false")
            return new Token(TokenType.FALSE, GetSubstr(), false);

        if (GetSubstr() == "PI")
            return new Token(TokenType.PI, GetSubstr(), Math.PI);

        if (GetSubstr() == "E")
            return new Token(TokenType.EULER, GetSubstr(), Math.E);

        foreach (string key in keywords.Keys)
        {
            if (GetSubstr() == key)
            {
                return new Token(keywords[key], GetSubstr());
            }
        }

        return new Token(TokenType.IDENTIFIER, GetSubstr());
    }

    private Token NextToken()
    {
        Token token = new Token(TokenType.EOF, "error");

        while (GetCurrent() == ' ')
            start = Advance();

        switch (GetCurrent())
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
            case '%':
                token = new Token(TokenType.MOD, "%");
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

        if (Char.IsDigit(GetCurrent()))
            token = NumberMatch();

        if (Char.IsLetter(GetCurrent()) || GetCurrent() == '_')
            token = IdentifierMatch();

        if (GetCurrent() == '#')
            token.Lexeme = "#";

        if (token.Lexeme == "error")
            Error.Report(ErrorType.LEXICAL_ERROR, "'" + GetSubstr() + "' is not valid token.");

        start = Advance();

        return token;
    }
}
