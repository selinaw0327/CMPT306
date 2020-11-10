using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    private Camera cam;

    public GameObject map;
    public Vector3Int minMapPosition;
    public Vector3Int maxMapPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(CameraCoroutine());
    }

    IEnumerator CameraCoroutine()
    {
        yield return new WaitForSeconds(1);
        minMapPosition = map.GetComponent<Tilemap>().origin;
        maxMapPosition = map.GetComponent<Tilemap>().origin + map.GetComponent<Tilemap>().size;
    }

    void LateUpdate()
    {
        // targetPosition vector is created to fix the camera's z position so it never pops through the ground
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, cam.transform.position.z);

        if (cam.transform.position != target.position)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, smoothing);
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, minMapPosition.x + 10.5f, maxMapPosition.x - 10.5f);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minMapPosition.y + 5.0f, maxMapPosition.y - 5.0f);

    }
}
