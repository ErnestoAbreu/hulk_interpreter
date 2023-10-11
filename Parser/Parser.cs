namespace hulk_interpreter;

public class Parser
{
    private readonly List<Token> tokens;
    int current = 0;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    /* Devuelve el token actual */
    private Token GetToken()
    {
        return tokens[current];
    }

    /* Devuelve el tipo del token actual */
    private new TokenType GetType()
    {
        return tokens[current].Type;
    }

    /* Devuelve la cadena de caracteres del token actual */
    private string GetLexeme()
    {
        return tokens[current].Lexeme;
    }

    /* Devuelve verdadero si alguno de los tipos que pasan por parametros coincide con el tipo actual */
    private bool Match(params TokenType[] tokentypes)
    {
        if (GetType() == TokenType.EOF)
            return false;

        foreach (TokenType type in tokentypes)
            if (GetType() == type)
                return true;

        return false;
    }

    /* Avanza al token siguiente */
    private int Advance()
    {
        if (GetType() != TokenType.EOF)
            current++;

        return current - 1;
    }

    /* Verifica si el tipo actual coincide con el tipo esperado */
    private int Consume(TokenType type, string lexeme)
    {
        if (Match(type))
            return Advance();

        throw new Error(ErrorType.SYNTAX_ERROR, "Expect '" + lexeme + "' in", current, tokens);
    }

    public Expression Parse()
    {
        string delete = "";
        try
        {
            if (Match(TokenType.FUNCTION))
            {
                Advance();
                string identifier = "";

                if (Match(TokenType.IDENTIFIER))
                    identifier = GetLexeme();

                Consume(TokenType.IDENTIFIER, "a name");

                Consume(TokenType.LEFT_PAREN, "(");

                List<string> arguments = new List<string>();
                while (Match(TokenType.IDENTIFIER))
                {
                    arguments.Add(GetLexeme());
                    Advance();
                    if (Match(TokenType.COMMA))
                        Advance();
                }

                Consume(TokenType.RIGHT_PAREN, ")");

                Consume(TokenType.INLINE_FUN, "=>");

                if (Functions.Contains(identifier))
                {
                    throw new Exception("Functions cannot be redefined.");
                }
                Functions.Add(identifier);
                delete = identifier;

                Expression body = expression();

                Consume(TokenType.SEMICOLON, ";");

                Functions.Add(identifier, new Function(identifier, arguments, body));

                return Functions.Get(identifier);
            }

            Expression expr = expression();

            if(current == tokens.Count - 1)
                Consume(TokenType.SEMICOLON, ";");

            if (GetType() == TokenType.EOF)
                return expr;
            else
                throw new Error(ErrorType.SYNTAX_ERROR, "Invalid syntax in ", current, tokens);
        }
        catch (Error error)
        {
            if (delete != "")
                Functions.Erase(delete);

            error.Report();
            return null!;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            return null!;
        }
    }

    private Expression expression()
    {
        return logical();
    }

    private Expression logical()
    {
        Expression expr = equality();

        while (Match(TokenType.AND, TokenType.OR))
        {
            Token token = GetToken();
            Advance();
            Expression right = equality();
            expr = new Binary(expr, token, right);
        }

        return expr;
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

        while (Match(TokenType.PRODUCT, TokenType.DIVISION, TokenType.MOD))
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
        Expression expr = unary();

        while (Match(TokenType.POWER))
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
            Expression right = unary();
            return new Unary(token, right);
        }

        return statement();
    }

    private List<Assing> assing()
    {
        List<Assing> assings = new List<Assing>();

        while (GetType() != TokenType.IN)
        {
            string identifier;
            if (Match(TokenType.IDENTIFIER))
            {
                identifier = GetLexeme();
                Advance();
            }
            else
                throw new Error(
                    ErrorType.SYNTAX_ERROR,
                    "Expect a variable name in",
                    current,
                    tokens
                );

            Consume(TokenType.EQUAL, "=");
            Expression expr = expression();

            if (!Match(TokenType.IN))
            {
                Consume(TokenType.COMMA, ",");

                if (Match(TokenType.IN))
                    throw new Error(
                        ErrorType.SYNTAX_ERROR,
                        "Expect a variable name in",
                        current,
                        tokens
                    );
            }

            assings.Add(new Assing(identifier, expr));
        }

        return assings;
    }

    private Expression statement()
    {
        if (Match(TokenType.IF))
        {
            Advance();
            Expression condition = primary();
            Expression ifBody = expression();
            Consume(TokenType.ELSE, "else");
            Expression elseBody = expression();
            return new IfStatement(condition, ifBody, elseBody);
        }

        if (Match(TokenType.LET))
        {
            Advance();
            List<Assing> assingBody = assing();
            Consume(TokenType.IN, "in");
            Expression body = expression();
            return new LetStatement(assingBody, body);
        }

        if (Match(TokenType.IDENTIFIER))
        {
            if (Functions.Contains(GetLexeme()))
            {
                string identifier = GetLexeme();
                Advance();
                Consume(TokenType.LEFT_PAREN, "(");

                List<Expression> arguments = new List<Expression>();
                while (GetType() != TokenType.RIGHT_PAREN)
                {
                    Expression argument = expression();

                    arguments.Add(argument);

                    if (GetType() != TokenType.RIGHT_PAREN)
                        Consume(TokenType.COMMA, ")");
                }

                Consume(TokenType.RIGHT_PAREN, ")");

                return new Call(identifier, arguments, Functions.Get(identifier));
            }

            Variable expr = new Variable(GetLexeme());
            Advance();
            return expr;
        }

        return primary();
    }

    private Expression primary()
    {
        if (
            Match(
                TokenType.NUMBER,
                TokenType.STRING,
                TokenType.FALSE,
                TokenType.TRUE,
                TokenType.PI,
                TokenType.EULER
            )
        )
            return new Literal(tokens[Advance()].literal);

        if (Match(TokenType.LEFT_PAREN))
        {
            Advance();
            Expression expr = expression();
            Consume(TokenType.RIGHT_PAREN, ")");
            return expr;
        }

        throw new Error(ErrorType.SYNTAX_ERROR, "Invalid syntax in", current, tokens);
    }
}
