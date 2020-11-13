using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [System.Serializable]
public class EnemyData 
{
    public int maxHealth;
    public int currentHealth;

    
    public int damage;

    public float[] position;

    public EnemyData(EnemyStats enemyStats){
        position = new float[2];
        position[0] = enemyStats.transform.position.x;
        position[1] = enemyStats.transform.position.y;

        maxHealth = enemyStats.maxHealth;
        currentHealth = enemyStats.currentHealth;
        damage = enemyStats.damage;
        

    }

 
}
