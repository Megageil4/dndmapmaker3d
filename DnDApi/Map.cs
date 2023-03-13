namespace DnDApi;

public class Map
{
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public List<float[]> Vertices { get; set; }
    public int[] Triangles { get; set; }

    public Map(List<float[]> vertices, int[] triangles)
    {
        Vertices = vertices;
        Triangles = triangles;
    }

    public override string ToString()
    {
        return $"{SizeY} {SizeX}";
    }
}