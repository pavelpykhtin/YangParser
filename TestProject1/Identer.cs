namespace TestProject1;

public class Identer: IDisposable
{
    public static int Ident { get; private set; }

    public Identer()
    {
        Ident++;
    }

    public void Dispose()
    {
        Ident--;
    }

    public static string GetIdent() => new(' ', Ident * 2);
}