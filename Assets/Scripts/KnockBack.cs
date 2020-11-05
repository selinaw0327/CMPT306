using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D enemy = GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                Vector2 difference = enemy.transform.position - other.gameObject.transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);

                PlayerStats playerStats = other.GetComponent<PlayerStats>();
                int damage = gameObject.GetComponentInChildren<EnemyStats>().damage;
                playerStats.TakeDamage(damage);
            }
        }
        if (other.gameObject.CompareTag("enemy") && gameObject.GetComponent<PlayerMovement>().currentState == PlayerState.attack)
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null)
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
}
