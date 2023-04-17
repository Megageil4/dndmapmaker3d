
using DefaultNamespace;

public class MapEventArgs
{
    public MapData Map { get; set; }

    public MapEventArgs(MapData map) : base()
    {
        Map = map;
    }
}