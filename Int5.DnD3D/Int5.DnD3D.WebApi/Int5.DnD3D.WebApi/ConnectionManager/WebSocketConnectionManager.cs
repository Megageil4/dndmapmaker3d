using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;

namespace FinalTest.ConnectionManager
{
    public class WebSocketConnectionManager
    {
        // Speichert alle Connections mit zugehöriger Guid
        public ConcurrentDictionary<Guid, WebSocket> Sockets { get; }
        public WebSocketConnectionManager()
        {
            Sockets = new ConcurrentDictionary<Guid, WebSocket>();
        }
        // Added einen WebSocket zum Connectionmanager und returned die erstellte Guid für die Connection
        public Guid AddWebSocket(WebSocket webSocket)
        {
            Guid guid = Guid.NewGuid();
            Sockets[guid] = webSocket;
            return guid;
        }
        // Sendet eine Nachricht an Alle Offenen Connections außer der der 
        // der übergebenen Guid gleich ist
        public async void anAlle(string text, string clientGuid)
        {

            foreach (var guid in Sockets.Keys)
            {
                if (guid != Guid.Parse(clientGuid))
                {
                    var socket = Sockets[guid];
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
                }
            }
        }

    }
}
