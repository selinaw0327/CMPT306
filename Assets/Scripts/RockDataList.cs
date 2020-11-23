using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RockDataList 
{
    public List<RockData> rockDataList = new List<RockData>();
    public List<RockData> smallRockOneDataList = new List<RockData>();
    public List<RockData> smallRockTwoDataList = new List<RockData>();

    public RockDataList(RockList rockList)
    {
        rockDataList = rockList.rockDataList;
        smallRockOneDataList = rockList.smallRockOneDataList;
        smallRockTwoDataList = rockList.smallRockTwoDataList;
    }
}
