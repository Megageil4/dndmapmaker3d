using UnityEngine;
using UnityEngine.UIElements;

namespace MapTools
{
    /// <summary>
    /// Tool used to move objects in the map
    /// </summary>
    public class Select : IMapTool
    {
        /// <summary>
        /// Camara used to cast the rays
        /// </summary>
        public Camera Camera { get; set; }
        /// <summary>
        /// The tag of the object that is being moved
        /// </summary>
        private string _lastHit;
        /// <summary>
        /// A bool that is used to check if the user is holding down the mouse
        /// </summary>
        private bool _isHoldingDown;
        /// <summary>
        /// The object that is currently selected
        /// </summary>
        public GameObject Selected;
        /// <summary>
        /// A delay that is used to make sure the user doesn't move the object too fast
        /// </summary>
        private int _delay;
        /// <summary>
        /// An offset that is used to make sure the object doesn' move too early when the user clicks
        /// </summary>
        private const double _AREAOFDEATH = .25;

        public Select(Camera camera)
        {
            Camera = camera;
        }

        public void ChangeMap(Vector3 location)
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
            {
                _isHoldingDown = false;
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
                if (_delay < 1)
                {
                    _delay++;
                    return;
                }
                _delay = 0;
                var position = Selected.transform.position;
                Vector3 mousePosition = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(position, Camera.transform.position ) - 1));
                Vector3 delta = mousePosition - position;
                
                if ((delta.x < _AREAOFDEATH && delta.x > -_AREAOFDEATH) && (delta.y < _AREAOFDEATH && delta.y > -_AREAOFDEATH))
                {
                    return;
                }
                switch (_lastHit)
                {
                    case "X":
                        Selected.transform.Translate(Mathf.CeilToInt(delta.x), 0, 0);
                        break;
                    case "Y":
                        Selected.transform.Translate(0, Mathf.CeilToInt(delta.y), 0);
                        break;
                    case "Z":
                        Selected.transform.Translate(0, 0, Mathf.CeilToInt(delta.z));
                        break;
                    case "All":
                    case "PrefabObject":
                        Selected.transform.Translate(Mathf.CeilToInt(delta.x), 0, Mathf.CeilToInt(delta.z));
                        break;
                }
                
                Debug.Log(Selected);
                foreach (var keys in DataContainer.Guids.Keys)
                {
                    Debug.Log("Key: " + keys);
                }
                DataContainer.Conn.ChangeGameObject(DataContainer.Guids[Selected]);
            }
        }
    }
}