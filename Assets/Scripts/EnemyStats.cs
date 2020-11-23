using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public StatBar healthBar;
    public int maxHealth;
    public int currentHealth;

     public bool isBoss;

    private EnemyLists enemyLists;
    private string enemyName;

    public int damage;

    public GameObject damageText;
    public bool getHit;
    private bool criticalHit;
    private Animator animator;
    private bool deathAnimationFound;

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

        criticalHit = false;
        animator = transform.parent.gameObject.GetComponent<Animator>();
        deathAnimationFound = HasParameter("isDead", animator);
        getHit = false;
    }

    void Update()
    {
        healthBar.SetStat(currentHealth, maxHealth);
    }

    // Causes the enemy to take damage
    public void TakeDamage(int damage) {

        getHit = true;
        Debug.Log("Current health:"+ currentHealth + "Damage: "+ damage);
        // Set current health and check if the enemy has died
       
       // calculate damage 
        CalcDamage();

        // Show the enemy's damage 
        if (damageText){
            ShowDamageText();
        }

        if(currentHealth <= 0) {
            if(isBoss) {
                if(enemyName.Equals("zombie")) {
                    if(enemyLists.wormList.Count == 0) {
                        SetDeathAnimation();
                        DestroyEnemy();
                    }
                }
                else if(enemyName.Equals("Skeleton")) {
                    if(enemyLists.ratList.Count == 0) {
                        SetDeathAnimation();
                        DestroyEnemy();
                    }
                }
                else if(enemyName.Equals("Vampire")) {
                    if(enemyLists.batDataList.Count == 0) {
                        SetDeathAnimation();
                        DestroyEnemy();
                    }
                } 
            }
            else {
                SetDeathAnimation();
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

                DestroyEnemy();
            }
        }
        // Debug.Log("New Health: "+ currentHealth);
        healthBar.SetStat(currentHealth, maxHealth);
    }

    public void ShowDamageText(){

        var damageTextObject = Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);

        // critical hit 
        if(criticalHit){
            var damageAmount = damage * 2;
            var TextMeshObject = damageTextObject.GetComponentInChildren<TextMesh>();
            TextMeshObject.text = "-" + damageAmount.ToString();
            TextMeshObject.color = Color.yellow;
            TextMeshObject.fontSize = 26;

        }
        // normal damage
        else{
            damageTextObject.GetComponentInChildren<TextMesh>().text = "-" + damage.ToString();           
        }


        Destroy(damageTextObject, 3f);

    }

    public void DestroyEnemy(){

        // play death aniamtion if there exits one
        if(deathAnimationFound){
            Destroy(this.transform.parent.gameObject, 3f);
        }
        else{
            Destroy(this.transform.parent.gameObject);
        }

    }

    public void CalcDamage(){
        float randValue = Random.value;
        if (randValue < .20f) 
            {
                criticalHit = true;
                currentHealth -= damage;
            }
        else 
            {   
                criticalHit = false;
                currentHealth -= damage * 2;
            }
        }

    public bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
        if (param.name == paramName)
            return true;
        }
        return false;
    }

    public void SetDeathAnimation(){

        if(deathAnimationFound){
            animator.SetBool("isDead", true);
        }
    }

      
}
