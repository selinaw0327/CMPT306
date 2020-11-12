using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLists : MonoBehaviour
{

    public List<GameObject> batList = new List<GameObject>();
    public List<EnemyData> batDataList = new List<EnemyData>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("enemy");

        foreach(GameObject enemy in enemyArray){
            if(enemy.name == "Bat"){
                batList.Add(enemy);

            }
        }
    }

}
