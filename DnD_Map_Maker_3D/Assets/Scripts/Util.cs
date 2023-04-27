using System.Xml.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Util
    {
        public static MapData MeshToMapData(Mesh mesh,int sizeX, int sizeY)
        {
            MapData mapData = new()
            {
                Triangles = mesh.triangles,
                Vertices = new()
            };
            foreach (var vertex in mesh.vertices)
            {
                mapData.Vertices.Add(new []{vertex.x,vertex.y,vertex.z});
            }

            mapData.sizeX = sizeX;
            mapData.sizeY = sizeY;

            return mapData;
        } 
    }
}