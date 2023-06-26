using System;
using MapTools;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    /// <summary>
    /// Class to handle clicks when a tool is selected
    /// </summary>
    public class ToolController : MonoBehaviour
    {
        /// <summary>
        /// Mesh filter of the map used to determine collisions of raycasts with the map
        /// </summary>
        public MeshFilter meshFilter;
        /// <summary>
        /// Camera used to cast the ray
        /// </summary>
        private Camera _camera;
        /// <summary>
        /// Delay to prevent multiple clicks in a short time
        /// </summary>
        private int _delay;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }
        
        /// <summary>
        /// The update cycle in which the raycast is cast and the tool is used
        /// </summary>
        private void Update()
        {
            _delay++;
            if (Input.GetMouseButton((int)MouseButton.LeftMouse))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.DrawRay(ray.origin,ray.direction,Color.red,100f);
                //if (eventSystem.IsPointerOverGameObject()) return;
                //if (eventSystem.currentSelectedGameObject != null) return;
                if (Toolbar.MapTool is Select || Toolbar.MapTool is Paint)
                {
                    Toolbar.MapTool.ChangeMap(new Vector3(0,0,0));
                    return;
                }
                if (_delay < 5)
                {
                    return;
                }

                _delay = 0;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Check if the raycast hit the mesh
                    if (hit.collider.gameObject == meshFilter.gameObject)
                    {
                        // Get the mouse position on the mesh
                        Vector3 mousePosition = hit.point;

                        // Convert the mouse position to local space
                        mousePosition = meshFilter.transform.InverseTransformPoint(mousePosition);
                        try
                        {
                            Toolbar.MapTool.ChangeMap(mousePosition);
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                        }
                        
                        Debug.Log($"Mouse position on mesh: {Mathf.FloorToInt(mousePosition.x)} / {Mathf.FloorToInt(mousePosition.z)}");
                    }
                }
            }
        }
    }
}