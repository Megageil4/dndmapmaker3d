using FinalTestClient.DnDWebSocketClient;

Console.WriteLine("I am a WebSocketClient");
WebSocketClient client = new();
int count = 0;
client.NewMap += (sender, e) =>
{
    count++;
    CreateFile(@"../../../Results/" + (count-1),"nm");
    
};
client.NewGameObject += (sender, e) =>
{
    count++;
    CreateFile(@"../../../Results/" + (count - 1), "ngo");
};
client.NewGuid += (sender, e) =>
{
    string path = @"../../../Results/Guid";
    string cont = e.Id + "";
    CreateFile(path, cont);
};

await client.Connect("ws://10.0.207.7:5180/ws");



while (true)
{

}

static void CreateFile(string path, string cont)
{
    using (FileStream fsDatei = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None))
    {
        using (BinaryWriter writer = new BinaryWriter(fsDatei))
        {
            writer.Write(cont);
        }
    }
}