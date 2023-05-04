// See https://aka.ms/new-console-template for more information
using System.Net.WebSockets;
using System.Text;


Console.WriteLine("press enter......");
Console.ReadLine();

using (ClientWebSocket client = new ClientWebSocket())
{
    Uri serviceUri = new Uri("ws://localhost:5020/ws");
    var cTs = new CancellationTokenSource();
    cTs.CancelAfter(TimeSpan.FromSeconds(120));
    try
    {
        await client.ConnectAsync(serviceUri, cTs.Token);
        var n = 0;
        while(client.State == WebSocketState.Open)
        {           
            var responseBuffer = new byte[1024];
            var offset = 0;
            var packet = 1024;
            while (true)
            {
                ArraySegment<byte> byteRecieved = new ArraySegment<byte>(responseBuffer, offset, packet);
                WebSocketReceiveResult response = await client.ReceiveAsync(byteRecieved, cTs.Token);
                var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                Console.WriteLine(responseMessage);
                if (response.EndOfMessage) break;

            }
            
        }
    } catch(WebSocketException e)
    {
        Console.WriteLine(e.Message);
    }
}

Console.ReadLine();
