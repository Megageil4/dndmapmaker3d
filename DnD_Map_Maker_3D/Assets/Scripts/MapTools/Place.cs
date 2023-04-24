using DnD_3D.ServerConnection.Default;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class Place : MonoBehaviour, IMapTool
    {
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
                GameObject gameObject = Instantiate(Controller.Prefab, new Vector3(posX,0,posZ), Quaternion.identity);
                DataContainer.Conn.AddGameObject(gameObject);
            }    
        }
        
    }
}