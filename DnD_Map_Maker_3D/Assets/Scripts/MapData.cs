
using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    [Serializable]
    public class MapData
    {
        public List<float[]> Vertices;
        public int[] Triangles;
        public int sizeX;
        public int sizeY;
    }
}