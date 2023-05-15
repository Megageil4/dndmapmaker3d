using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Paint : IMapTool
    {
        private Camera _playerCamera;
        public Paint(Camera playerCamera)
        {
            _playerCamera = playerCamera;
        }

        public void ChangeMap(Vector3 location)
        {
            GameObject selected;
            try
            {
                selected = Util.GetGameObjektByRayCast(_playerCamera);
            }
            catch (Exception e)
            {
                Debug.Log("No Object selected for painting");
                return;
            }

            
            selected.transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}