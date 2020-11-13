﻿using System.Collections;
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
        currentHealth = maxHealth;
        healthBar.SetMaxStat(maxHealth);
        healthBar.transform.position = transform.position + healthBar.offset;
    }

    // Causes the enemy to take damage
    public void TakeDamage(int damage) {
        // Set current health and check if the enemy has died
        currentHealth -= damage;
        if(currentHealth <= 0) {
            transform.parent.gameObject.GetComponent<EnemyDrop>().Drop();
            Destroy(this.transform.parent.gameObject);
        }
        healthBar.SetStat(currentHealth, maxHealth);
    }
}
