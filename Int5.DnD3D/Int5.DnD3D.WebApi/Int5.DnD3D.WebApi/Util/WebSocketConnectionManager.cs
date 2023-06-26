using Int5.DnD3D.WebApi.Model;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;

namespace FinalTest.ConnectionManager
{
    public class WebSocketConnectionManager
    {
        // Speichert alle Connections mit zugehöriger Guid
        public ConcurrentDictionary<Guid, User> Connections { get; }
        public WebSocketConnectionManager()
        {
            Connections = new ConcurrentDictionary<Guid, User>();
        }
        
        public void KillConnection(Guid guid)
        {
            Connections.Remove(guid, out _);
            AnAlle("np", Guid.NewGuid()+"");
        }
        
        // Added einen WebSocket zum Connectionmanager und returned die erstellte Guid für die Connection
        
        
        public Guid AddUser(User user)
        {
            Guid guid = Guid.NewGuid();
            Connections[guid] = user;
            return guid;
        }
        
        // Sendet eine Nachricht an Alle Offenen Connections außer der der 
        // der übergebenen Guid gleich ist
        public async Task AnAlle(string text, string clientGuid)
        {

            foreach (var guid in Connections.Keys)
            {
                if (guid != Guid.Parse(clientGuid))
                {
                    var socket = Connections[guid].WebSocket;
                    var cTs = new CancellationTokenSource();
                    cTs.CancelAfter(TimeSpan.FromHours(2));
                    if (socket.State == WebSocketState.Open)
                    {
                        string message = text;
                        if (!string.IsNullOrEmpty(message))
                        {

                            var byteToSend = Encoding.UTF8.GetBytes(message);
                            Console.WriteLine(message);
                            await socket.SendAsync(byteToSend, WebSocketMessageType.Text, true, cTs.Token);
                        }
                    }
                    else
                    {
                        KillConnection(guid);
                    }
                }
            }
        }

    }
}
