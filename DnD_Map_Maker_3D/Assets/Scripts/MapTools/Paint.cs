using System;
using HSVPicker;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Paint : IMapTool
    {
        private Camera _playerCamera;
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
            catch (Exception e)
            {
                Debug.Log("No Object selected for painting");
                return;
            }


            selected.transform.GetChild(0)
                              .GetChild(0)
                              .gameObject
                              .GetComponent<MeshRenderer>()
                              .material.color = _colorPicker
                                                .GetComponent<ColorPicker>()
                                                .CurrentColor;

            DataContainer.Conn.ChangeGameObject(DataContainer.Guids[selected]);
        }
    }
}