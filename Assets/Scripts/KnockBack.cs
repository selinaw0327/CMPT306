using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    static private bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        invincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("enemy") && other.gameObject.CompareTag("enemy"))
        {
            // if two enemies collide, do nothing
            return;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            // if the enemy collides with the player, the player takes damage and is knocked back
            Rigidbody2D enemy = GetComponent<Rigidbody2D>();
            if (enemy != null && !other.isTrigger)
            {
                Vector2 difference = enemy.transform.position - other.gameObject.transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);

                if (!invincible)
                {
                    PlayerStats playerStats = other.GetComponent<PlayerStats>();
                    int damage = gameObject.GetComponentInChildren<EnemyStats>().damage;
                    playerStats.TakeDamage(damage);
                    StartCoroutine(InvinsibleCo());
                }
            }
        }
        else if (other.gameObject.CompareTag("enemy") && gameObject.GetComponent<PlayerMovement>().currentState == PlayerState.attack)
        {
            // if the enemy is hit by the player's sword hitbox, the enemy is knocked back and takes damage
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            PlayerStats playerStats = GetComponent<PlayerStats>();
            if (enemy != null && playerStats.swordEquipped && !other.isTrigger)
            {
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);

                EnemyStats enemyStats = other.gameObject.GetComponentInChildren<EnemyStats>();
                int damage = GetComponent<PlayerStats>().damage;
                enemyStats.TakeDamage(damage);
            }
        }
    }

    IEnumerator InvinsibleCo()
    {
        invincible = true;
        yield return new WaitForSeconds(0.75f); // amount of time it takes for another enenmy to be able to attack
        invincible = false;
    }
}
