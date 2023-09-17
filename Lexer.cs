using System.Text.RegularExpressions;

namespace hulk_interpreter;

public class Lexer
{
    private string code = "";
    private int start = 0;
    private int current = 0;
    private bool Scanned = false;
    private List<Token> Tokens = new List<Token>();

    public Lexer(string code)
    {
        this.code = code;
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

    private Token NextToken()
    {
        Token token = new Token(TokenType.EOF, "");

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
            case '.':
                token = new Token(TokenType.DOT, ".");
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
        }

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
}
