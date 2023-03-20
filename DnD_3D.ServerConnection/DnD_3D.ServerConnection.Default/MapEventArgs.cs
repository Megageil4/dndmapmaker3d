using DnD_3D.ServerConnection.Entity;

namespace DnD_3D.ServerConnection.Default;

public class MapEventArgs
{
    Map Map { get; set; }

    public MapEventArgs(Map map) : base()
    {
        Map = map;
    }
}