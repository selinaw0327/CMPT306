using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCamera : MonoBehaviour
{
    public GameObject grid, map, mapCamera, canvas, image, renderTexture;
    private Vector3 mapSize;
    private Vector3 mapCenter;

    private float mapX;
    private float mapY;
     
    private float minX, maxX, minY, maxY;
    private bool mapOn = false;

     void Start()
     {        
        StartCoroutine(LateStart(2.0f));
     }
 
     IEnumerator LateStart(float waitTime)
     {
        yield return new WaitForSeconds(waitTime);

        map = GameObject.Find("Map");
        mapCamera = GameObject.Find("MapCamera");
        canvas = GameObject.Find("MapBackground");
        image = GameObject.Find("MapImage");
        renderTexture = GameObject.Find("MapRenderTexture");  

        mapSize = grid.GetComponent<ScanGrid>().getMapSize();
        mapX = mapSize.x;
        mapY = mapSize.y;
        mapCenter = grid.GetComponent<ScanGrid>().getMapCenter();

        calcCameraSize();
     }

     
     void calcCameraSize() {

        transform.position = new Vector3(mapCenter.x, mapCenter.y, -1f);
        
        if (mapY> mapX){
            mapCamera.GetComponent<Camera>().orthographicSize = mapY/2 + 10;
        }
        else{
            float screenAspect = (float) Screen.width / (float) Screen.height;
            float height = mapX/screenAspect + 10;
            mapCamera.GetComponent<Camera>().orthographicSize = height;

        }
        
     }
     

     void Update(){
         this.GetComponent<Camera>().Render();
         if (Input.GetKeyDown("m")){
             if (!mapOn){
                Debug.Log("m pressed!");
                canvas.GetComponent<Image>().enabled = true;
                renderTexture.GetComponent<RawImage>().enabled = true;
                mapOn = true;
             }
             else{
                 canvas.GetComponent<Image>().enabled = false;
                 renderTexture.GetComponent<RawImage>().enabled = false;
                 mapOn = false;
             }
         }
     }

  
}
