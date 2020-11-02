using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public Vector2 boundaryMin;
    public Vector2 boundaryMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam = Camera.main.GetComponent<CameraMovement>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") &&
            (cam.minPosition.x + cameraChange.x > boundaryMin.x && cam.minPosition.y + cameraChange.y > boundaryMin.y) &&
            (cam.maxPosition.x + cameraChange.x < boundaryMax.x && cam.maxPosition.y + cameraChange.y < boundaryMax.y))
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
        }
    }
}
