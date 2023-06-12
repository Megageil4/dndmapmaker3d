using System;
using System.Xml.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// Class containing utility functions
    /// </summary>
    public class Util : MonoBehaviour
    {
        /// <summary>
        /// Method to convert a mesh to a MapData object
        /// </summary>
        /// <param name="mesh">The mesh to convert</param>
        /// <param name="sizeX">The x size of the map</param>
        /// <param name="sizeY">The y size of the</param>
        /// <returns>A MapData object made from the Mesh</returns>
        public static MapData MeshToMapData(Mesh mesh,int sizeX, int sizeY)
        {
            MapData mapData = new()
            {
                triangles = mesh.triangles,
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
        
        /// <summary>
        /// Gets the game object that the raycast hit
        /// The raycast is made from the mouse position
        /// </summary>
        /// <param name="playerCamera">The camara the ray is send from</param>
        /// <returns>The gameobject that got hit</returns>
        /// <exception cref="Exception">Gets thrown if no object was found</exception>
        public static GameObject GetGameObjektByRayCast(Camera playerCamera)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 100f);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Check if the raycast hit the mesh
                if (hit.transform.CompareTag("PrefabObject"))
                {
                    return hit.transform.gameObject;
                }
            }
            
            throw new Exception("No object found");
        }
        
        public static void Exit()
        {
            if (DataContainer.WebserviceConnection != null)
            {
                DataContainer.WebserviceConnection.Close();   
            }
            Application.Quit();
        }
    }
}