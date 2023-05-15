using System;
using System.Xml.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Util : MonoBehaviour
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
    }
}