using FinalTestClient.DnDWebSocketClient;



Console.WriteLine("I am a WebSocketClient");
WebSocketClient client = new();
int count = 0;
Guid clientID;
string tempPath = Path.GetTempPath() + @"DnD\";
Directory.CreateDirectory(tempPath);
client.NewMap += (_, _) =>
{
    CreateFile("nm");
    
};
client.NewGameObject += (_, _) =>
{
    CreateFile("ngo");
};
client.NewGuid += (_, e) =>
{
  string cont = e.Id + "";
  clientID = e.Id;
  CreateFile(cont);
  tempPath += cont + "/";
  Directory.CreateDirectory(tempPath);
	Console.WriteLine(tempPath);
};
client.NewPlayer += (_, _) =>
{
  CreateFile("np");
};



await client.Connect($"ws://{args[0]}:443/ws?username={args[1]}");




while (true)
{

}

void CreateFile(string cont)
{
    count++;
    using (FileStream fsDatei = File.Open(tempPath + (count - 1), FileMode.Create, FileAccess.Write, FileShare.None))
    {
        using (BinaryWriter writer = new BinaryWriter(fsDatei))
        {
            writer.Write(cont);
        }
    }
}