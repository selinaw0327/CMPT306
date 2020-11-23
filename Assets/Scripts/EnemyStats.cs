using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public StatBar healthBar;
    public int maxHealth;
    public int currentHealth;
    public int damage;

    public GameObject damageText;
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
        criticalHit = false;
        animator = transform.parent.gameObject.GetComponent<Animator>();
        deathAnimationFound = HasParameter("isDead", animator);
        Debug.Log("Hello!!");
        Debug.Log("Death animation exits? " + deathAnimationFound);
    }

    void Update()
    {
        healthBar.SetStat(currentHealth, maxHealth);
        
    }

    // Causes the enemy to take damage
    public void TakeDamage(int damage) {
        Debug.Log("Current health:"+ currentHealth + "Damage: "+ damage);
        // Set current health and check if the enemy has died
       
       // calculate damage 
        CalcDamage();

        // Show the enemy's damage 
        if (damageText){
            ShowDamageText();
        }

        if(currentHealth <= 0) {

            SetDeathAnimation();
            // if the enemy is not in tutorial scene, run enemy item drop script
            if (!transform.parent.gameObject.GetComponent<EnemyDrop>().tutorial)
            {
                transform.parent.gameObject.GetComponent<EnemyDrop>().Drop();
            }

            if (this.transform.parent.gameObject.name == "Bat"){
                ChallengeMenu challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
                challengeMenu.updateChallenge("5bat");
            }
            DestroyEnemy();
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
