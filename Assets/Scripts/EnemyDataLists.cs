using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyDataLists
{
    public List<EnemyData> batDataList = new List<EnemyData>();

    public EnemyDataLists(EnemyLists enemyLists)
    {
        batDataList = enemyLists.batDataList;
    }
}