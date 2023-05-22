using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Class to handle the movement of the camera
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// The camara that is moved
    /// </summary>
    public new Camera camera;
    /// <summary>
    /// Last position of the mouse
    /// </summary>
    private Vector3 _lastPosition;
    /// <summary>
    /// Current z position of the camera
    /// </summary>
    private float _currentZ;

    // Start is called before the first frame update
    void Start()
    {
        _currentZ = camera.transform.position.z;
        transform.position = new Vector3(1, 5, 0 + _currentZ);
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
            Transform transform1;
            (transform1 = camera.transform).Rotate(new Vector3(1,0,0),  -delta.y * 360);
            // todo Qaternion limitieren, aaaaa das sollte doch gehen 
            // camera.transform.rotation = new Quaternion( Mathf.Clamp(camera.transform.rotation.x, -90, 90),
                // camera.transform.rotation.y, camera.transform.rotation.z, camera.transform.rotation.w);
            // Debug.Log(camera.transform.eulerAngles.x);
            
            // Horizontales drehen
            var position = transform1.position;
            camera.transform.transform.Rotate(new Vector3(position.x, 100, position.y), delta.x * 360, Space.World);
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
