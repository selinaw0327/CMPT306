using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ScanGrid : MonoBehaviour

{
    public GameObject Grid;
    public GameObject map;

    private Vector3Int size;
    private Vector3 center;
    private Vector3 mapCenter;

   void Start()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 0.1 seconds.
        yield return new WaitForSeconds(1.5f);
        
        calcMapSize();
        setAGridDimensions();
       
        AstarPath.active.Scan();
        
    }

    private void calcMapSize(){
        size = map.GetComponent<Tilemap>().size;
        center = map.GetComponent<Tilemap>().cellBounds.center;
    }

    private void setAGridDimensions(){
        var width = size.x;
        var depth = size.y;
        var nodeSize = 1.0f;
        var gg = AstarPath.active.data.gridGraph;
        gg.SetDimensions(width, depth, nodeSize);
        gg.center = new Vector3(center.x, center.y, center.z);
        mapCenter = gg.center;

    }

    public Vector3Int getMapSize(){
        return size;
    }

    public Vector3 getMapCenter(){
        return mapCenter;
    }

}
