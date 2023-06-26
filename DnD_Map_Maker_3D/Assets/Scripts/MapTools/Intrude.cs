using UnityEngine;

namespace MapTools
{
    /// <summary>
    /// Tool to intrude squares the map
    /// </summary>
    public class Intrude : IMapTool
    {
        /// <summary>
        /// The mesh spawner to change values in the mesh
        /// </summary>
        public PlaneSpawner Spawner { get; set; }

        public Intrude(PlaneSpawner spawner)
        {
            Spawner = spawner;
        }

        public void ChangeMap(Vector3 location)
        {
            var posX = Mathf.FloorToInt(location.x);
            var posZ = Mathf.FloorToInt(location.z);
            Spawner.vertices[(posZ * (Spawner.sizeX + 1)) + posX].y += -0.5f;
            Spawner.vertices[(posZ * (Spawner.sizeX + 1)) + posX + 1].y += -0.5f;
            Spawner.vertices[((posZ + 1) * (Spawner.sizeX + 1)) + posX].y += -0.5f;
            Spawner.vertices[((posZ + 1) * (Spawner.sizeX + 1)) + posX + 1].y += -0.5f;
            Debug.Log(posZ * Spawner.sizeY + posX);
            Spawner.ReloadMesh();
        }
    }
}