using System.Net.WebSockets;
using System.Text;

Console.Title = "Client";

using var ws = new ClientWebSocket();

await ws.ConnectAsync(new Uri("ws://localhost:5215/ws"), CancellationToken.None);
var buf = new byte[1056];

while (ws.State == WebSocketState.Open)
{
    var result = await ws.ReceiveAsync(buf, CancellationToken.None);

    if (result.MessageType == WebSocketMessageType.Close)
    {
        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
        Console.WriteLine(result.CloseStatusDescription);
    }
    else
    {
        Console.WriteLine(Encoding.UTF8.GetString(buf, 0, result.Count));
    }
    
}