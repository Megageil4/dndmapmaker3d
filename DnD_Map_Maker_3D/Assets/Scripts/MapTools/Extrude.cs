
using UnityEngine;

namespace DefaultNamespace
{
    public class Extrude : IMapTool
    {
        public PlaneSpawner Spawner { get; set; }

        public Extrude(PlaneSpawner spawner)
        {
            Spawner = spawner;
        }

        public void ChangeMap(Vector3 location)
        {
            var mesh = Spawner.GetComponent<MeshFilter>().mesh;
            var posX = Mathf.RoundToInt(location.x);
            var posZ = Mathf.RoundToInt(location.z);
            mesh.vertices[posZ * Spawner.sizeX + posX].y += 1;
            Spawner.ReloadMesh();
        }
    }
}