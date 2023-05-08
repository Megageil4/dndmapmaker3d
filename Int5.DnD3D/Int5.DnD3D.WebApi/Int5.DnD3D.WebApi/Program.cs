using FinalTest.Controllers;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var wsOptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromMinutes(2) };
app.UseWebSockets(wsOptions);
// Handelt einkommende Http anfragen welche dem Pfad "/ws" folgen
// und den RequestType WebSocketRequest haben
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await Echo(context, webSocket);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            
        }
    }
    else
    {
        await next(context);
    }
});

static async Task Echo(HttpContext context,WebSocket webSocket)
{
   
    var buffer = new byte[1024 * 4];
    Guid guid = DnDController._connectionManager.AddWebSocket(webSocket);
    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(guid + "")), WebSocketMessageType.Text, 0, CancellationToken.None);
    Console.WriteLine("Neuer Client " + guid);
    while (webSocket.State == WebSocketState.Open) { }
}

app.UseAuthorization();

app.MapControllers();

app.Run();
