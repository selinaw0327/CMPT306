using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class PlayerData 
{

    public int Health;
    public int Damage;

    public bool swordEquipped;
    public int overallHealth;
    public  string sword;
    public float[] position; 
    

    

    public PlayerData(PlayerStats player){

        Health = player.currentHealth;
        Damage = player.overallDamage;
        overallHealth = player.overallHealth;
        swordEquipped = player.swordEquipped;
        sword = player.sword;
        
        
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

    }
}
