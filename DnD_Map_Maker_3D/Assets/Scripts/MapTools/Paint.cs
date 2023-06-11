using System;
using DefaultNamespace;
//using HSVPicker;
using UnityEngine;

namespace MapTools
{
    /// <summary>
    /// Tool to paint objects on the map
    /// </summary>
    public class Paint : IMapTool
    {
        /// <summary>
        /// Player camera to shoot the raycast
        /// </summary>
        private Camera _playerCamera;
        /// <summary>
        /// Color picker used to get the selected color
        /// </summary>
        private GameObject _colorPicker;
        public Paint(Camera playerCamera, GameObject colorPicker)
        {
            _playerCamera = playerCamera;
            _colorPicker = colorPicker;
        }

        public void ChangeMap(Vector3 location)
        {
            GameObject selected;
            try
            {
                selected = Util.GetGameObjektByRayCast(_playerCamera);
            }
            catch (Exception)
            {
                Debug.Log("No Object selected for painting");
                return;
            }


            // selected.transform.GetChild(0)
            //                   .GetChild(0)
            //                   .gameObject
            //                   .GetComponent<MeshRenderer>()
            //                   .material.color = _colorPicker
            //                                     .GetComponent<ColorPicker>()
            //                                     .CurrentColor;

            DataContainer.Conn.ChangeGameObject(DataContainer.Guids[selected]);
        }
    }
}