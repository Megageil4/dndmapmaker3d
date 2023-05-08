namespace FinalTest.Model
{
    public class Map
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public List<float[]> Vertices { get; set; }
        public int[] Triangles { get; set; }

        public Guid ClientId { get; set; }
        public Map(List<float[]> vertices, int[] triangles,Guid clientId)
        {
            Vertices = vertices;
            Triangles = triangles;
            ClientId = clientId;
        }

        public override string ToString()
        {
            return $"{SizeY} {SizeX}";
        }

    }
}
