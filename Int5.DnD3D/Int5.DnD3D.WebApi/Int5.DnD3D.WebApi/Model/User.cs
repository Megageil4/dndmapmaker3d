using System.Net.WebSockets;

namespace Int5.DnD3D.WebApi.Model
{
    public class User
    {
        public string Username { get; set; }
        public WebSocket WebSocket { get; set; }

        public User(string username,WebSocket webSocket)
        {
            Username = username;
            WebSocket = webSocket;
        }
    }
}
