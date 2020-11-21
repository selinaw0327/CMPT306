using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public StatBar healthBar;
    public int maxHealth;
    public int currentHealth;

    
    public int damage;

    // Start is called before the first frame update
    void Start()
    {   
        if(currentHealth == 0){
            currentHealth = maxHealth;
        }
        healthBar.SetMaxStat(maxHealth);
        healthBar.transform.position = transform.position + healthBar.offset;
    }

    void Update()
    {
        healthBar.SetStat(currentHealth, maxHealth);
        
    }

    // Causes the enemy to take damage
    public void TakeDamage(int damage) {
        Debug.Log("Current health:"+ currentHealth + "Damage: "+ damage);
        // Set current health and check if the enemy has died
        currentHealth -= damage;
        if(currentHealth <= 0) {

            // if the enemy is not in tutorial scene, run enemy item drop script
            if (!transform.parent.gameObject.GetComponent<EnemyDrop>().tutorial)
            {
                transform.parent.gameObject.GetComponent<EnemyDrop>().Drop();
            }

            if (this.transform.parent.gameObject.name == "Bat"){
                ChallengeMenu challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
                challengeMenu.updateChallenge("5bat");
            }

            Destroy(this.transform.parent.gameObject);
            EnemyLists enemyLists = GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>();
            List<GameObject> objectList;
            
            
            if(this.name == "Bat"){
                objectList = enemyLists.batList;
                
            } else {
                
                objectList = new List<GameObject>();
            }
            objectList.Remove(this.transform.parent.gameObject);
            
        }
        Debug.Log("New Health: "+ currentHealth);
        healthBar.SetStat(currentHealth, maxHealth);
    }
}
