using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _center;
    public new Camera camera;
    private Vector3 _lastPosition;
    private float _currentZ;

    // Start is called before the first frame update
    void Start()
    {
        _currentZ = camera.transform.position.z;
        _center = new Vector3(0, 0, 0);
        transform.position = new Vector3(_center.x, _center.y, _center.z + _currentZ);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown((int) MouseButton.LeftMouse) || Input.GetMouseButtonDown((int) MouseButton.RightMouse))
        {
            _lastPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton((int) MouseButton.LeftMouse))
        {
            Vector3 delta = _lastPosition - camera.ScreenToViewportPoint(Input.mousePosition);

            transform.position = _center;
            camera.transform.Rotate(new Vector3(1,0,0), delta.y * 360);
            camera.transform.transform.Rotate(new Vector3(_center.x, 100, _center.y), delta.x * 360, Space.World);
            camera.transform.Translate(new Vector3(0,0, _currentZ));
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
            _center += new Vector3(delta.x * 5, delta.y * 5,0);
            _lastPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
