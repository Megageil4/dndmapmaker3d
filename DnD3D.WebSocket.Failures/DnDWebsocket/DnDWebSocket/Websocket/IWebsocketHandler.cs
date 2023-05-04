using System.Net.WebSockets;

namespace DnDWebSocket.Websocket;

public interface IWebsocketHandler
{
    Task Handle(Guid id, WebSocket webSocket);
}