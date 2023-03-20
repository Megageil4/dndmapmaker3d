using DnD_3D.ServerConnection.Entity;

namespace DnD_3D.ServerConnection.Default;

public class MapEventArgs
{
    public Map Map { get; set; }

    public MapEventArgs(Map map) : base()
    {
        Map = map;
    }
}