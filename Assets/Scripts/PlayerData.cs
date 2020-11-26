using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class PlayerData 
{

    public int Health;
    public int Hunger;
    public int Energy;
    public int Damage;

    public bool swordEquipped;
    public int maxhealth;
    public int maxhunger;
    public int maxenergy;
    public  string sword;
    public float[] position; 
    

    

    public PlayerData(PlayerStats player){

        Hunger = player.currentHunger;
        Health = player.currentHealth;
        Energy = player.currentEnergy;
        Damage = player.damage;
        maxhealth = player.maxHealth;
        maxenergy = player.maxEnergy;
        maxhunger = player.maxHunger;
        swordEquipped = player.swordEquipped;
        sword = player.sword;
        
        
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

    }
}
