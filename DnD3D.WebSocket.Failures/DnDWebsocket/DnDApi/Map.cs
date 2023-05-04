namespace DnDApi;

public class Map
{
    public List<float[]> Vertices { get; set; }
    public int[] Triangles { get; set; }

    public Map(List<float[]> vertices, int[] triangles)
    {
        Vertices = vertices;
        Triangles = triangles;
    }

    public override string ToString()
    {
        return $"{Vertices.Count} {Triangles.Length}";
    }
}