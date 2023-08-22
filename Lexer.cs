namespace hulk_interpreter;

public class Lexer {
    private string Code = "";
    private List<Token> Tokens = new List<Token>();

    public Lexer(string code) {
        this.Code = code;
    }

    public List<Token> GetTokens() {

        return Tokens;
    }
}