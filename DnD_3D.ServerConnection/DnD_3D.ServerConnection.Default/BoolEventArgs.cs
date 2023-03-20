namespace DnD_3D.ServerConnection.Default
{
    public class BoolEventArgs : EventArgs
    {
        public bool ergebnis { get; set; }

        public BoolEventArgs(bool ergebnis)
        {
            this.ergebnis = ergebnis;
        }
    }
}