using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    private Camera cam;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        // minPosition.x = tilemap.localBounds.
        Vector3 tilemapMax = tilemap.localBounds.max;
        Vector3 tilemapMin = tilemap.localBounds.min;
    }

    void LateUpdate()
    {
        // targetPosition vector is created to fix the camera's z position so it never pops through the ground
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, cam.transform.position.z);

        // targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        // targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        if(cam.transform.position != target.position)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, smoothing);
        }

    }
}
