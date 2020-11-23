using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public StatBar healthBar;
    public int maxHealth;
    public int currentHealth;
    
    public int damage;

    public bool isBoss;

    private EnemyLists enemyLists;
    private string enemyName;

    // Start is called before the first frame update
    void Start()
    {   
        if(currentHealth == 0){
            currentHealth = maxHealth;
        }
        healthBar.SetMaxStat(maxHealth);
        healthBar.transform.position = transform.position + healthBar.offset;

        enemyLists = GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>();
        enemyName = this.transform.parent.name;
    }

    void Update()
    {
        healthBar.SetStat(currentHealth, maxHealth);
        
    }

    // Causes the enemy to take damage
    public void TakeDamage(int damage) {
        // Debug.Log("Current health:"+ currentHealth + "Damage: "+ damage);
        // Set current health and check if the enemy has died
        currentHealth -= damage;
        if(currentHealth <= 0) {
            
            if(isBoss) {
                if(enemyName.Equals("zombie")) {
                    if(enemyLists.wormList.Count == 0) {
                        Destroy(this.transform.parent.gameObject);
                    }
                }
                else if(enemyName.Equals("Skeleton")) {
                    if(enemyLists.ratList.Count == 0) {
                        Destroy(this.transform.parent.gameObject);
                    }
                }
                else if(enemyName.Equals("Vampire")) {
                    if(enemyLists.batDataList.Count == 0) {
                        Destroy(this.transform.parent.gameObject);
                    }
                } 
            }
            else {
                // if the enemy is is the boss room, run ememny boss room drop script
                if(transform.parent.gameObject.GetComponent<EnemyDrop>().bossRoom) {
                    transform.parent.gameObject.GetComponent<EnemyDrop>().BossRoomDrop();
                }
                // if the enemy is not in tutorial scene, run enemy item drop script
                else if (!transform.parent.gameObject.GetComponent<EnemyDrop>().tutorial)
                {
                    transform.parent.gameObject.GetComponent<EnemyDrop>().Drop();
                }

                if (this.transform.parent.gameObject.name == "Bat"){
                    ChallengeMenu challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
                    challengeMenu.updateChallenge("5bat");
                }

                List<GameObject> objectList;

                // Find specific enemy list and remove the enemy from that list
                if(enemyName.Equals("Worm")) {
                    objectList = enemyLists.wormList;
                }
                else if(enemyName.Equals("Rat")) {
                    objectList = enemyLists.ratList;
                }
                else if(enemyName.Equals("Bat")) {
                    objectList = enemyLists.batList;
                } 
                else {
                    objectList = new List<GameObject>();
                }
                objectList.Remove(this.transform.parent.gameObject);

                Destroy(this.transform.parent.gameObject);
            }
        }
        // Debug.Log("New Health: "+ currentHealth);
        healthBar.SetStat(currentHealth, maxHealth);
    }
}
