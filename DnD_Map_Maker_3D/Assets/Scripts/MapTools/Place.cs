using UnityEngine;
using UnityEngine.UIElements;

namespace MapTools
{
    /// <summary>
    /// Tool to place objects on the map
    /// </summary>
    public class Place : MonoBehaviour, IMapTool
    {
        /// <summary>
        /// The controller of the object menu used to get the selected object. 
        /// </summary>
        private ObjectController Controller { get; set; }

        public Place(ObjectController controller)
        {
            Controller = controller;
        }

        public void ChangeMap(Vector3 location)
        {
            if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
            {
                var posX = Mathf.FloorToInt(location.x);
                var posZ = Mathf.FloorToInt(location.z);
                GameObject instance = Instantiate(Controller.Prefab, new Vector3(posX,0,posZ), Quaternion.identity);
                DataContainer.Conn.AddGameObject(instance);
            }    
        }
        
    }
}