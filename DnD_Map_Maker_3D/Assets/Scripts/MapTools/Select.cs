using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class Select : IMapTool
    {
        private Vector3 _lastPosition;
        public Camera Camera { get; set; }
        private string _lastHit;
        private bool _isHoldingDown;
        public GameObject Selected;
        private const int _modifier = 10;
        private int _delay = 0;

        public Select(Camera camera)
        {
            Camera = camera;
        }

        public void ChangeMap(Vector3 location)
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
            {
                _isHoldingDown = false;
                _lastPosition = Camera.ScreenToViewportPoint(Input.mousePosition);
                Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction, Color.red, 100f);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Check if the raycast hit the mesh
                    if (hit.transform.CompareTag("PrefabObject"))
                    {
                        if (Selected != null)
                        {
                            Selected.transform.GetChild(1).gameObject.SetActive(false);
                        }
                        hit.transform.GetChild(1).gameObject.SetActive(true);
                        Selected = hit.transform.gameObject;
                    }

                    if (hit.transform.CompareTag("X") || hit.transform.CompareTag("Y") 
                        || hit.transform.CompareTag("Z") || hit.transform.CompareTag("All")
                        || hit.transform.CompareTag("PrefabObject"))
                    {
                        _lastHit = hit.transform.tag;
                        _isHoldingDown = true;
                    }
                    
                }
            }
            else if (Input.GetMouseButton((int)MouseButton.LeftMouse) && _isHoldingDown)
            {
                if (_delay < 2300 * Time.deltaTime)
                {
                    _delay++;
                    return;
                }
                _delay = 0;
                Vector3 delta = Camera.ScreenToViewportPoint(Input.mousePosition) - _lastPosition;
                if (delta.x == 0 && delta.y == 0)
                {
                    return;
                }
                float camx = Camera.transform.right.x;
                float camy = Camera.transform.forward.x;
                int newpos = Mathf.RoundToInt((camx + camy) * Mathf.Sign(delta.x));
                switch (_lastHit)
                {
                    case "X":
                        Selected.transform.Translate(newpos,0,0);
                        break;
                    case "Y":
                        Selected.transform.Translate(0, Mathf.Sign(delta.y),0);
                        break;
                    case "Z":
                        Selected.transform.Translate(0,0,-newpos);
                        break;
                    case "All":
                    case "PrefabObject":
                        Vector3 mousePosition = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.transform.position.y));
                        delta = mousePosition - Selected.transform.position;
                        Selected.transform.Translate(Mathf.CeilToInt(delta.x), 0, Mathf.CeilToInt(delta.z));
                        break;
                }

                _lastPosition = Camera.ScreenToViewportPoint(Input.mousePosition);
            }
        }
    }
}