
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
            var posX = Mathf.FloorToInt(location.x);
            var posZ = Mathf.FloorToInt(location.z);
            Spawner.Vertices[(posZ * (Spawner.sizeY + 1)) + posX].y += 0.5f;
            Spawner.Vertices[(posZ * (Spawner.sizeY + 1)) + posX + 1].y += 0.5f;
            Spawner.Vertices[(posZ * (Spawner.sizeY + 1) + Spawner.sizeY) + 2 + posX].y += 0.5f;
            Spawner.Vertices[(posZ * (Spawner.sizeY + 1) + Spawner.sizeY + 1) + posX].y += 0.5f;
            Debug.Log(posZ * Spawner.sizeY + posX);
            Spawner.ReloadMesh();
        }
    }
}