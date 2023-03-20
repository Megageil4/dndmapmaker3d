
using DefaultNamespace;

public class MapEventArgs
{
    MapData Map { get; set; }

    public MapEventArgs(MapData map) : base()
    {
        Map = map;
    }
}