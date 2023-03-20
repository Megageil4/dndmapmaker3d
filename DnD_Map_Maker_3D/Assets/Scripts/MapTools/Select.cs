using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class Select : IMapTool
    {
        public Camera Camera { get; set; }

        public Select(Camera camera)
        {
            Camera = camera;
        }

        public void ChangeMap(Vector3 location)
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
            {
                Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 100f);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Check if the raycast hit the mesh
                    if (hit.transform.CompareTag("PrefabObject"))
                    {
                        
                    }
                }
            }
        }
    }
}