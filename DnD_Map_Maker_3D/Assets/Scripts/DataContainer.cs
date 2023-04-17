
public class DataContainer
{
    public static string ServerIP { get; set; }
    public static IDnDConnection Conn { get; private set; }

    public static void CreateConn(IDnDConnection con)
    {
        Conn = con;
    }
}
