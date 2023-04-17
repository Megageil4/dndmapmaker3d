using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class ToolController : MonoBehaviour
    {
        public MeshFilter meshFilter;
        private Camera _camera;
        private Mesh _mesh;
        private int _delay;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _mesh = meshFilter.mesh;
        }

        private void Update()
        {
            _delay++;
            if (Input.GetMouseButton((int)MouseButton.LeftMouse))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.DrawRay(ray.origin,ray.direction,Color.red,100f);
                if (Toolbar.MapTool is Select)
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