using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockList : MonoBehaviour
{
    
    public List<RockData> rockDataList = new List<RockData>();
    public List<GameObject> rockList = new List<GameObject>();

    public List<RockData> smallRockOneDataList = new List<RockData>();
    public List<GameObject> smallRockOneList = new List<GameObject>();

    public List<RockData> smallRockTwoDataList = new List<RockData>();
    public List<GameObject> smallrockTwoList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] rockArray = GameObject.FindGameObjectsWithTag("Rock");
        
        foreach(GameObject rock in rockArray){

            switch (rock.name) {
                case "Large Rock":
                    rockList.Add(rock);
                    break;
                case "Small Rock One":
                    smallRockOneList.Add(rock);
                    break;
                case "Small Rock Two":
                    smallrockTwoList.Add(rock);
                    break;
            }
        }
    }


}