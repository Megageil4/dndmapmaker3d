using DnDWebSocket.Websocket;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IWebsocketHandler, WebsocketHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseWebSockets();
app.UseEndpoints(routes =>
{
    routes.MapControllerRoute(
        name: "default",
        pattern: "{controller=Page}/{action=Index}/{id?}");
});
/*
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var buf = new byte[1056];
        while (true)
        {
            var eingabe = await webSocket.ReceiveAsync(buf, CancellationToken.None);
            var data = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(buf,0,eingabe.Count));
            //await webSocket.SendAsync(data,WebSocketMessageType.Text,true,CancellationToken.None);
        }
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});


async Task<string> ReceiveMessage(string test, WebSocket ws)
{
    var
}
    */

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();