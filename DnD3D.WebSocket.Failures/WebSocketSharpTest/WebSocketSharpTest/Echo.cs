using WebSocketSharp;
using WebSocketSharp.Server;
namespace WebSocketSharpTest
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Recieved message from client: " +e.Data);
            Sessions?.Broadcast(e.Data);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            
        }

        protected override void OnOpen()
        {
            base.OnOpen();
        }
    }
}
