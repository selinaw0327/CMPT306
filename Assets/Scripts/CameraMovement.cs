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
    public Vector3Int minPosition;
    public Vector3Int maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(CameraCoroutine("Pits"));
    }

    IEnumerator CameraCoroutine(string currentMap)
    {
        yield return new WaitForSeconds(2);
        map = GameObject.Find(currentMap);
        minPosition = map.GetComponent<Tilemap>().origin;
        maxPosition = map.GetComponent<Tilemap>().origin + map.GetComponent<Tilemap>().size;
    }

    void LateUpdate()
    {
        // targetPosition vector is created to fix the camera's z position so it never pops through the ground
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, cam.transform.position.z);

        if (cam.transform.position != target.position)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, smoothing);
        }

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x + 10.5f, maxPosition.x - 10.5f);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y + 5.0f, maxPosition.y - 5.0f);
    }

    public void UpdatePlayerReference(string currentMap) {
        if (currentMap != null)
        {
            StartCoroutine(CameraCoroutine(currentMap));
        }
        
    }
}

