using System.Collections.Generic;

namespace DefaultNamespace
{
    public class MapData
    {
        public List<float[]> vertices { get; set; }
        public int[] triangles { get; set; }

        public MapData()
        {
            this.vertices = new List<float[]>();
            this.triangles = triangles;
        }
    }
}