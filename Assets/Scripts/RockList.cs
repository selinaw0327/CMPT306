using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockList : MonoBehaviour
{
    
    public List<RockData> rockDataList = new List<RockData>();
    public List<GameObject> rockList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] rockArray = GameObject.FindGameObjectsWithTag("Rock");
        
        foreach(GameObject rock in rockArray){
            
            rockDataList.Add(new RockData(rock));
            rockList.Add(rock);
            
        }
    }


}