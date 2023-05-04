using System.Net.WebSockets;
using DnDWebSocket.Websocket;
using Microsoft.AspNetCore.Mvc;

namespace DnDWebSocket.Controllers;

[Route("/ws")]
public class StreamController : Controller
{
    public IWebsocketHandler WebsocketHandler { get; }

    public StreamController(IWebsocketHandler websocketHandler)
    {
        WebsocketHandler = websocketHandler;
    }

    [HttpGet]
    public async Task Get()
    {
        var context = ControllerContext.HttpContext;
        var isSocketRequest = context.WebSockets.IsWebSocketRequest;

        if (isSocketRequest)
        {
            WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();

            await WebsocketHandler.Handle(Guid.NewGuid(), websocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
}