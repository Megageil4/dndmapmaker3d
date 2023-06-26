using FinalTest.Controllers;
using Microsoft.Data.Sqlite;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;

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
if (!File.Exists(@"user.db"))
{
    using (var connection = new SqliteConnection(@"Data Source=user.db"))
    {
        connection.Open();
        var command = connection.CreateCommand();
        command = connection.CreateCommand();
        command.CommandText = @"
        CREATE TABLE User (
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            username TEXT NOT NULL
        );
        INSERT INTO User
        VALUES (0, 'default');
        ";
        command.ExecuteNonQuery();
        Console.WriteLine("Datenbank neu erstellt");
    }
}
var wsOptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromMinutes(2) };
app.UseWebSockets(wsOptions);
//Handelt einkommende Http anfragen welche dem Pfad "/ws" folgen
//und den RequestType WebSocketRequest haben
app.Use(async (context, next) =>
{
    //if (Regex.IsMatch(context.Request.Path,"[^/ws]"))
    if(context.Request.Path.StartsWithSegments("/ws"))
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var parameterParts = (context.Request.Path + "").Split("/");
            var parameter = parameterParts[2];
            if (DnDController._databaseManager.UserExists(parameter))
            {
                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket, parameter);
            }
            else
            {
                Console.WriteLine("Response 403");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
        }
        else
        {
            Console.WriteLine("Response 400");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            
        }
    }
    else
    {
        await next(context);
    }
});

static async Task Echo(WebSocket webSocket,string username)
{
    var buffer = new byte[1024 * 4];
    Guid guid = DnDController._connectionManager.AddUser(new Int5.DnD3D.WebApi.Model.User(username, webSocket));
    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(guid + "")), WebSocketMessageType.Text, 0, CancellationToken.None);
    DnDController._connectionManager.AnAlle("np", guid+"");
    Console.WriteLine("Neuer Client " + guid + ": " + username);
    while (webSocket.State == WebSocketState.Open)
    {
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine(message);
    }
}

/*static async Task Echo(HttpContext context,WebSocket webSocket)
{
   
    var buffer = new byte[1024 * 4];
    Guid guid = DnDController._connectionManager.AddWebSocket(webSocket);
    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(guid + "")), WebSocketMessageType.Text, 0, CancellationToken.None);
    Console.WriteLine("Neuer Client " + guid);
    while (webSocket.State == WebSocketState.Open) {
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine(message);

    }
}*/

app.UseAuthorization();

app.MapControllers();

app.Run();
