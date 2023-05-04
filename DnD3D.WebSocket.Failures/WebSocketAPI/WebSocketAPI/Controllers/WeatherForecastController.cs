using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace WebSocketAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private static List<WebSocket> Connections { get; set; }
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        
        }

        public static  void AddWebSocket(WebSocket webSocket)
        {
            if (Connections is null) Connections = new();
            Connections.Add(webSocket);
            Console.WriteLine("Geadded");
            Console.WriteLine(Connections.Count);
        }

        private async void anAlle(string text)
        {
            Console.WriteLine("In anAlle()");
            Console.WriteLine(Connections.Count);
            foreach (var client in Connections)
            {
                Console.WriteLine("In while()");
                var cTs = new CancellationTokenSource();
                cTs.CancelAfter(TimeSpan.FromHours(2));
                if (client.State == WebSocketState.Open)
                {
                    Console.WriteLine("In if#1");
                    string message = text;
                    if (!string.IsNullOrEmpty(message))
                    {
                        Console.WriteLine("In if#2");
                        var byteToSend = Encoding.UTF8.GetBytes(message);
                        await client.SendAsync(byteToSend, WebSocketMessageType.Text, true, cTs.Token);
                        Console.WriteLine("Post SendAsync");
                        var responseBuffer = new byte[1024];
                        var offset = 0;
                        var packet = 1024;
                        while (true)
                        {
                            ArraySegment<byte> byteRecieved = new ArraySegment<byte>(responseBuffer, offset, packet);
                            WebSocketReceiveResult response = await client.ReceiveAsync(byteRecieved, cTs.Token);
                            var responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                            Console.WriteLine(responseMessage);
                            if (response.EndOfMessage) break;

                        }
                    }
                }
            }            
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            anAlle("nm");
            Console.WriteLine("Sofort test");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            
        }
    }
}