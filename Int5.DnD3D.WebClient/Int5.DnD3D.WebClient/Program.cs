using FinalTestClient.DnDWebSocketClient;



Console.WriteLine("I am a WebSocketClient");
WebSocketClient client = new();
int count = 0;
client.NewMap += (sender, e) =>
{
    CreateFile("nm");
    
};
client.NewGameObject += (sender, e) =>
{
    CreateFile("ngo");
};
client.NewGuid += (sender, e) =>
{
    string cont = e.Id + "";
    CreateFile(cont);
};



await client.Connect("ws://10.0.207.7:443/ws");




while (true)
{

}

void CreateFile(string cont)
{
    count++;
    using (FileStream fsDatei = File.Open(Path.GetTempPath() + (count - 1), FileMode.Create, FileAccess.Write, FileShare.None))
    {
        using (BinaryWriter writer = new BinaryWriter(fsDatei))
        {
            writer.Write(cont);
        }
    }
}