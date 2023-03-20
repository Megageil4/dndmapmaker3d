using DnD_3D.ServerConnection.Entity;

namespace DnD_3D.ServerConnection.Default
{
    public class GameObjectEventArgs : EventArgs
    {
        List<GameObject> GameObject { get; set; }

        public GameObjectEventArgs(List<GameObject> gameObject) : base()
        {
            GameObject = gameObject;
        }
    }
}