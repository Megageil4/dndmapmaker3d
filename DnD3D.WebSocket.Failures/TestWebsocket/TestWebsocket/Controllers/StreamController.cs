using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace TestWebsocket.Controllers;
[Route("/ws")]
public class StreamController : Controller
{
    [HttpGet]
    public async Task Get()
    {
        var context = ControllerContext.HttpContext;
        var isSocketRequest = context.WebSockets.IsWebSocketRequest;
        if (isSocketRequest)
        {
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            
        }
    }
}