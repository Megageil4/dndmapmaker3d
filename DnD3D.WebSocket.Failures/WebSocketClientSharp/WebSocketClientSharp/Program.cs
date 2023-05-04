using WebSocketSharp;

Console.Write("Bitte Namen eingeben: ");
string name = Console.ReadLine() ?? string.Empty;

using (WebSocket ws = new WebSocket("ws://127.0.0.1:5020/ws"))
{
    ws.OnMessage += (sender, e) =>
    {
        Console.WriteLine("Recieved from Server " + e.Data);
    };
    //ws.OnMessage += test;
    ws.Connect();
    
    while (true)
    {

        //string message = Console.ReadLine() ?? string.Empty;
        //ws.Send(name+": "+message);
    }
    
    
}

void test(object? sender, MessageEventArgs e)
{
    throw new NotImplementedException();
}