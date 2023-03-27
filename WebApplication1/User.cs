


using System.Net.WebSockets;

namespace WebApplication1;

public class User
{
    public string name { get; set; }
    public WebSocket ws;

    public User(string name)
    {
        this.name = name;
        ws = null;
    }
}