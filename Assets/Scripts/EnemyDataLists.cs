using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyDataLists
{
    public List<EnemyData> batDataList = new List<EnemyData>();
    public List<EnemyData> wormDataList = new List<EnemyData>();
    public List<EnemyData> ratDataList = new List<EnemyData>();

    public List<EnemyData> vampDataList = new List<EnemyData>();
    public List<EnemyData> skelDataList = new List<EnemyData>();
    public List<EnemyData> zombDataList = new List<EnemyData>();


    public EnemyDataLists(EnemyLists enemyLists)
    {
        batDataList = enemyLists.batDataList;
        wormDataList = enemyLists.wormDataList;
        ratDataList = enemyLists.ratDataList;
        vampDataList = enemyLists.vampDataList;
        zombDataList = enemyLists.zombDataList;
        skelDataList = enemyLists.skelDataList;
    }
}