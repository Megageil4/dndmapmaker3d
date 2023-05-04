using WebSocketSharp.Server;
WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7890");
wssv.Start();
Console.WriteLine("Server started on ws://127.0.0.1:7890");
Console.ReadKey();
wssv.Stop();