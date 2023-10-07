namespace hulk_interpreter;

enum Types
{
    NUMBER,
    STRING,
    BOOL,
    NULL
}

class Literal : Expression
{
    public object value;
    Types type;

    public Literal(object value, Types type = Types.NULL)
    {
        this.value = value;
        this.type = type;
    }
}
