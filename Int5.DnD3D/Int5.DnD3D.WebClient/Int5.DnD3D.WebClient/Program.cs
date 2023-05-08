using FinalTestClient.DnDWebSocketClient;

Console.WriteLine("I am a WebSocketClient");
WebSocketClient client = new();
client.NewMap += (sender, e) =>
{
    Console.WriteLine("New Map ausgelöst");
    // Get Aufruf von Map
};
client.NewGameObject += (sender, e) =>
{
    Console.WriteLine("New GameObject ausgelöst");
    // Get Aufruf von GameObjects
};
client.NewGuid += (sender, e) =>
{
    Console.WriteLine(e.Id);
    // Id abrufen und speichern
};

await client.Connect("ws://localhost:5020/ws");



while (true)
{

}