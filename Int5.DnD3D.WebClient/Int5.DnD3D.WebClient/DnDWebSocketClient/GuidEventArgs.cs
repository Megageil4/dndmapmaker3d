namespace Int5.DnD3D.WebClient.DnDWebSocketClient
{
    public class GuidEventArgs : EventArgs
    {
        public Guid Id { get; set; }
        public GuidEventArgs(Guid id)
        {
            Id = id;
        }
    }
}
