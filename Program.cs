
class Program
{
    static void Main(string[] args)
    {
        while (true) { 
            Console.Write(">");
            string? code = Console.ReadLine();
            if (code == null) break;
            Run(code);
        }
    }

    static void Run(string code){
       
    }
}
