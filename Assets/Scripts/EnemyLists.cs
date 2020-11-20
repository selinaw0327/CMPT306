using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLists : MonoBehaviour
{

    public List<GameObject> batList = new List<GameObject>();
    public List<EnemyData> batDataList = new List<EnemyData>();

    public List<GameObject> wormList = new List<GameObject>();
    public List<EnemyData> wormDataList = new List<EnemyData>();

    public List<GameObject> ratList = new List<GameObject>();
    public List<EnemyData> ratDataList = new List<EnemyData>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("enemy");

        foreach(GameObject enemy in enemyArray){
            switch (enemy.name) {
                case "Worm":
                    wormList.Add(enemy);
                    break;
                case "Rat":
                    ratList.Add(enemy);
                    break;
                case "Bat":
                    batList.Add(enemy);
                    break;
            }
        }
    }

}
