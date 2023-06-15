using Int5.DnD3D.WebClient.DnDWebSocketClient;
using System.Net.WebSockets;
using System.Text;

namespace FinalTestClient.DnDWebSocketClient;
/// <summary>
/// Base class for the WebSocketClient to handle the connection and messages
/// </summary>
public class WebSocketClient
{
    private ClientWebSocket webSocket;
    /// <summary>
    /// The Guid of the client
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Establisched die Connection und Recieved die Guid
    /// </summary>
    /// <param name="url">the url of the server</param>
    public async Task Connect(string url)
    {
        webSocket = new ClientWebSocket();
    
        await webSocket.ConnectAsync(new Uri(url), CancellationToken.None);
        byte[] buffer = new byte[1024];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        var guid = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Id = Guid.Parse(guid);
        OnNewGuid();
        await Task.WhenAll(Receive());
    }
    /// <summary>
    /// runs in the background, recieves messages and handles them
    /// </summary>
    private async Task Receive()
    {
        byte[] buffer = new byte[1024 * 4];
        while (webSocket.State == WebSocketState.Open)
        {
            try
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    switch (message)
                    {
                        case "nm":
                            OnNewMap();
                            break;
                        case "ngo":
                            OnNewGameObject();
                            break;
                        case "np":
                            OnNewPlayer();
                            break;
                    }
                    Console.WriteLine($"Received message: {message}");
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
            } catch {
                Console.WriteLine("Connection wurde geschlossen");
            }
        }
    }
    
  
    /// <summary>
    /// closes the connection
    /// </summary>
    public async Task Disconnect()
    {
        if (webSocket.State == WebSocketState.Open)
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            Console.WriteLine("Disconnected from WebSocket server");
        }
    }
    
    #region Event
    public event EventHandler<EventArgs> NewMap;
    public event EventHandler<EventArgs> NewGameObject;
    public event EventHandler<GuidEventArgs> NewGuid;
    public event EventHandler<EventArgs> NewPlayer;

    protected virtual void OnNewGuid()
    {
        NewGuid?.Invoke(this, new GuidEventArgs(Id));
    }
    protected virtual void OnNewMap()
    {
        NewMap?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void OnNewGameObject()
    {
        NewGameObject?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void OnNewPlayer()
	  {
      NewPlayer?.Invoke(this, EventArgs.Empty);
	  }
    #endregion
}
