using System.Net.WebSockets;
using System.Text;

using var ws = new ClientWebSocket();

await ws.ConnectAsync(new Uri("ws://localhost:5215/ws"), CancellationToken.None);
Console.WriteLine(ws.State);

    if (ws.State == WebSocketState.Open)
    {
        var data = Encoding.UTF8.GetBytes($"moinsen");
        await ws.SendAsync(data, WebSocketMessageType.Text,
            true, CancellationToken.None);
    }
    Console.WriteLine(ws.State);

//await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,"Fertig",CancellationToken.None);