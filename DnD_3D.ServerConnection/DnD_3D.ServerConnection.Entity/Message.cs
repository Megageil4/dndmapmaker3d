namespace DnD_3D.ServerConnection.Entity;

public class Message
{
    public string MessageType { get; set; }
    public string Guid { get; set; }

    public Message(string messageType, string guid)
    {
        MessageType = messageType;
        Guid = guid;
    }
}
