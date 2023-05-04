using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharpTest;

WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7890");
wssv.AddWebSocketService<Echo>("/Echo");
Console.WriteLine("Server started on ws://127.0.0.1:7890/Echo");
wssv.KeepClean = true;
wssv.WaitTime = TimeSpan.MaxValue;
wssv.Start();

Console.ReadKey();
Console.WriteLine(wssv.Log);
Console.ReadKey();