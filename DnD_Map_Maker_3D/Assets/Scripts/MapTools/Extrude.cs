﻿
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
            var posX = Mathf.RoundToInt(location.x);
            var posZ = Mathf.RoundToInt(location.z);
            Spawner.Vertices[(posZ * (Spawner.sizeY + 1)) + posX].y += 0.5f;
            Debug.Log(posZ * Spawner.sizeY + posX);
            Spawner.ReloadMesh();
        }
    }
}