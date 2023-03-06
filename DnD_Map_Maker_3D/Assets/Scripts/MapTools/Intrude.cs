﻿
using UnityEngine;

namespace DefaultNamespace
{
    public class Intrude : IMapTool
    {
        public PlaneSpawner Spawner { get; set; }

        public Intrude(PlaneSpawner spawner)
        {
            Spawner = spawner;
        }

        public void ChangeMap(Vector3 location)
        {
            var posX = Mathf.RoundToInt(location.x);
            var posZ = Mathf.RoundToInt(location.z);
            Spawner.Vertices[(posZ * (Spawner.sizeY + 1)) + posX].y -= 0.5f;
            Debug.Log(posZ * Spawner.sizeY + posX);
            Spawner.ReloadMesh();
        }
    }
}