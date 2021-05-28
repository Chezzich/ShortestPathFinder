using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Camera camera;
    private Vector3 origin;

    private const float CAMERA_DRAG_SPEED = 0.1f;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            camera.orthographicSize--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camera.orthographicSize++;
        }

        if (Input.GetMouseButtonDown(0))
        {
            origin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = camera.ScreenToViewportPoint(Input.mousePosition - origin);
        Vector3 move = new Vector3(pos.x * CAMERA_DRAG_SPEED, pos.y * CAMERA_DRAG_SPEED);

        camera.transform.Translate(move, Space.World);
    }
}
