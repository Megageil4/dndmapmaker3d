using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public new Camera camera;
    private Vector3 _lastPosition;
    private float _currentZ;

    // Start is called before the first frame update
    void Start()
    {
        _currentZ = camera.transform.position.z;
        transform.position = new Vector3(1, 0, 0 + _currentZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown((int) MouseButton.MiddleMouse) || Input.GetMouseButtonDown((int) MouseButton.RightMouse))
        {
            _lastPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton((int) MouseButton.MiddleMouse))
        {
            Vector3 delta = camera.ScreenToViewportPoint(Input.mousePosition) - _lastPosition;
            // Vertikales drehen
            camera.transform.Rotate(new Vector3(1,0,0),  delta.y * 360);
            // todo Qaternion limitieren, limits herausfinden 
            // Horizontales drehen
            camera.transform.transform.Rotate(new Vector3(camera.transform.position.x, 100, camera.transform.position.y), delta.x * 360, Space.World);
            _lastPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            _currentZ += Input.mouseScrollDelta.y;
            camera.transform.Translate(new Vector3(0,0,Input.mouseScrollDelta.y));
        }

        if (Input.GetMouseButton((int)MouseButton.RightMouse))
        {
            Vector3 delta = _lastPosition - camera.ScreenToViewportPoint(Input.mousePosition);
            camera.transform.Translate(new Vector3(delta.x * 5, delta.y * 5, 0));
            _lastPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
