using UnityEngine;

namespace DefaultNamespace
{
    public class Place : MonoBehaviour, IMapTool
    {
        public ObjectController Controller { get; set; }

        public Place(ObjectController controller)
        {
            Controller = controller; //TODO spawn prefab
        }

        public void ChangeMap(Vector3 location)
        {
            if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse))
            {
                Instantiate(Controller.Prefab, Vector3.zero, Quaternion.identity);
            }    
        }
        
    }
}