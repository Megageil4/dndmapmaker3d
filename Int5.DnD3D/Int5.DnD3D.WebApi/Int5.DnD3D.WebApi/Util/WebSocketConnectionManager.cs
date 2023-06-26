using Int5.DnD3D.WebApi.Model;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;

namespace FinalTest.ConnectionManager
{
    /// <summary>
    /// Manages the WebSocket Connection for multiple open Connections, aswell as the user that opened said connection
    /// </summary>
    public class WebSocketConnectionManager
    {
        // Speichert alle Connections mit zugehöriger Guid
        public ConcurrentDictionary<Guid, User> Connections { get; }
        /// <summary>
        /// Creates a new Instance of the WebSocketConnectionManager
        /// </summary>
        public WebSocketConnectionManager()
        {
            Connections = new ConcurrentDictionary<Guid, User>();
        }
        /// <summary>
        /// Removes a connection belonging to a session id from the connectionlist
        /// </summary>
        /// <param name="guid">session id of the to be removed connection</param>
        public void KillConnection(Guid guid)
        {
            Connections.Remove(guid, out _);
            MessageAll("np", Guid.NewGuid()+"");
        }
        
        // Added einen WebSocket zum Connectionmanager und returned die erstellte Guid für die Connection
        
        /// <summary>
        /// Adds a User to the connectionlist 
        /// </summary>
        /// <param name="user">User to be added</param>
        /// <returns>Session Id for the connection</returns>
        public Guid AddUser(User user)
        {
            Guid guid = Guid.NewGuid();
            Connections[guid] = user;
            return guid;
        }
        
        // Sendet eine Nachricht an Alle Offenen Connections außer der der 
        // der übergebenen Guid gleich ist
        /// <summary>
        /// Messages all open connections
        /// </summary>
        /// <param name="text">Text to be sent</param>
        /// <param name="clientGuid">Session Id to be ignored</param>
        /// <returns>Task</returns>
        public async Task MessageAll(string text, string clientGuid)
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
