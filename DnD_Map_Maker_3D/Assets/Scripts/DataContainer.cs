
public class DataContainer
{
    public static string ServerIP { get; set; }
    public static IDnDConnection Conn { get; private set; }

    static DataContainer()
    {
        ServerIP = "10.0.207.3";
    }
    
    public static void CreateConn(IDnDConnection con)
    {
        Conn = con;
    }
}
