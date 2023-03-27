using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using WebApplication1;

namespace WebSocketsSample.Controllers;

#region snippet_Controller_Connect
[ApiController]
public class WebSocketController : ControllerBase
{
    private  void  LoginRegistrieren(WebSocket ws, string uk)
    {
        User user = new(uk);
        user.ws = ws;
        Userverwaltung.def.AddUser(user);
    }
    [Route("/ws/login")]
    [HttpGet]
    public async Task Get([FromBody] string login)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await Echo(webSocket,login);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    #endregion

    private static async Task Echo(WebSocket webSocket,string login)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!receiveResult.CloseStatus.HasValue)
        {
            await webSocket.SendAsync(
                new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                receiveResult.MessageType,
                receiveResult.EndOfMessage,
                CancellationToken.None);

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }
}