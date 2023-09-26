using System.Text.RegularExpressions;

namespace hulk_interpreter;

public class Parser
{
    private readonly List<Token> tokens;
    int current = 0;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    private Token GetToken()
    {
        return tokens[current];
    }

    private new TokenType GetType()
    {
        return tokens[current].Type;
    }

    private string GetLexeme()
    {
        return tokens[current].Lexeme;
    }

    private bool Match(params TokenType[] tokentypes)
    {
        if (GetType() == TokenType.EOF)
            return false;

        foreach (TokenType type in tokentypes)
            if (GetType() == type)
                return true;

        return false;
    }

    private int Advance()
    {
        if (GetType() != TokenType.EOF)
            current++;

        return current - 1;
    }

    private int Consume(TokenType type)
    {
        if (Match(type))
        {
            Advance();
            return current - 1;
        }

        throw new Exception("Expect " + type + " after expression.");
    }

    public Expression Parse()
    {
        try
        {
            Expression expr = expression();
            if (GetType() == TokenType.SEMICOLON || GetType() == TokenType.EOF)
            {
                return expr;
            }
            else
            {
                Error.Report(ErrorType.SYNTAX_ERROR, "invalid syntax in " + GetLexeme());
                Error.hadError = true;
                return null!;
            }
        }
        catch (Exception e)
        {
            Error.Report(ErrorType.SYNTAX_ERROR, e.Message);
            Error.hadError = true;
            return null!;
        }
    }

    private Expression expression()
    {
        return equality();
    }

    private Expression equality()
    {
        Expression expr = comparison();

        while (Match(TokenType.EQUAL_EQUAL, TokenType.NOT_EQUAL))
        {
            Token token = GetToken();
            Advance();
            Expression right = comparison();
            expr = new Binary(expr, token, right);
        }

        return expr;
    }

    private Expression comparison()
    {
        Expression expr = concatenation();

        while (
            Match(TokenType.LESS, TokenType.LESS_EQUAL, TokenType.GREATER, TokenType.GREATER_EQUAL)
        )
        {
            Token token = GetToken();
            Advance();
            Expression right = concatenation();
            expr = new Binary(expr, token, right);
        }

        return expr;
    }

    private Expression concatenation()
    {
        Expression expr = term();

        while (Match(TokenType.AT))
        {
            Token token = GetToken();
            Advance();
            Expression right = term();
            expr = new Binary(expr, token, right);
        }

        return expr;
    }

    private Expression term()
    {
        Expression expr = factor();

        while (Match(TokenType.PLUS, TokenType.MINUS))
        {
            Token token = GetToken();
            Advance();
            Expression right = factor();
            expr = new Binary(expr, token, right);
        }

        return expr;
    }

    private Expression factor()
    {
        Expression expr = power();

        while (Match(TokenType.PRODUCT, TokenType.DIVISION))
        {
            Token token = GetToken();
            Advance();
            Expression right = power();
            expr = new Binary(expr, token, right);
        }

        return expr;
    }

    private Expression power()
    {
        Expression expr = logical();

        while (Match(TokenType.POWER))
        {
            Token token = GetToken();
            Advance();
            Expression right = logical();
            expr = new Binary(expr, token, right);
        }

        return expr;
    }

    private Expression logical()
    {
        Expression expr = unary();

        while (Match(TokenType.AND, TokenType.OR))
        {
            Token token = GetToken();
            Advance();
            Expression right = unary();
            expr = new Binary(expr, token, right);
        }

        return expr;
    }

    private Expression unary()
    {
        if (Match(TokenType.MINUS, TokenType.NOT))
        {
            Token token = GetToken();
            Advance();
            Expression right = statement();
            return new Unary(token, right);
        }

        return statement();
    }

    private Expression statement()
    {
        if (Match(TokenType.IF))
        {
            Advance();
            Expression condition = primary();
            Expression ifBody = expression();
            Consume(TokenType.ELSE);
            Expression elseBody = expression();
            return new IfStatement(condition, ifBody, elseBody);
        }

        return primary();
    }
    private Expression primary()
    {
        if (Match(TokenType.NUMBER, TokenType.STRING, TokenType.FALSE, TokenType.TRUE))
            return new Literal(tokens[Advance()].literal);

        if (Match(TokenType.LEFT_PAREN))
        {
            Advance();
            Expression expr = expression();
            Consume(TokenType.RIGHT_PAREN);
            return expr;
        }

        throw new Exception(tokens[current].ToString());
    }

}
