
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    /// <summary>
    /// Class used to send map between server and client to prevent
    /// property overhead from mesh
    /// </summary>
    [Serializable]
    public class MapData
    {
        /// <summary>
        /// List of all vertices in the map
        /// </summary>
        public List<float[]> Vertices;
        /// <summary>
        /// List of all triangles connecting the vertices in the map
        /// </summary>
        [FormerlySerializedAs("Triangles")] public int[] triangles;
        /// <summary>
        /// The x size of the map
        /// </summary>
        public int sizeX;
        /// <summary>
        /// The y(z) size of the map
        /// </summary>
        public int sizeY;
        /// <summary>
        /// The client of the current session 
        /// </summary>
        public Guid ClientID;
        public Guid ClientId = DataContainer.ClientId;
    }
}