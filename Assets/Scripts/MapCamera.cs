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


    void Awake() {
        map.SetActive(true);
    }

     void Start()
     {
        canvas.GetComponent<Image>().enabled = false;
        renderTexture.GetComponent<RawImage>().enabled = false;
        
        StartCoroutine(LateStart(1f));
         
     }
 
     IEnumerator LateStart(float waitTime)
     {
         yield return new WaitForSeconds(waitTime);
         mapSize = grid.GetComponent<ScanGrid>().getMapSize();
         mapX = mapSize.x;
         mapY = mapSize.y;
         mapCenter = grid.GetComponent<ScanGrid>().getMapCenter();
         calcCameraSize();
         
     }

     
     void calcCameraSize() {
        transform.position = new Vector3(mapCenter.x, mapCenter.y, -1f);
        if (mapX == 0 && mapY == 0){
            mapCamera.GetComponent<Camera>().orthographicSize = 20;    // For tutorial scene where PCG is not used for the map
        }
        
        else if(mapY> mapX){
            mapCamera.GetComponent<Camera>().orthographicSize = mapY/2 + 10;
        }
        else{
            float ratio = mapCamera.GetComponent<Camera>().aspect;
            float height = mapX/ratio;
            mapCamera.GetComponent<Camera>().orthographicSize = height/2 + 10;
        }
    

    
        
     }
     

     void Update(){
         this.GetComponent<Camera>().Render();
         if (Input.GetKeyDown("m")){
             if (!mapOn){
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
