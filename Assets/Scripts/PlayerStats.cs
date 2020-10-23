using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    // Variables needed for healthbar
    public HealthBar healthBar;
    public int maxHealth;
    public int currentHealth;
    public int currentHunger;
    public int currentLevel;
    public int damage;
    public int currentEnergy;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set current and max health, as well as position of healthbar
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.transform.position = transform.position + healthBar.offset; // Places healthbar above players head based on its offset
    }

    // Update is called once per frame
    void Update()
    {
        // Simple test to take damage
        if(Input.GetKeyDown(KeyCode.Space) && currentHealth > 0) {
            TakeDamage(10);
        }
        // Test to heal player
        if(Input.GetKeyDown(KeyCode.H) && currentHealth < maxHealth) {
            Heal(10);
        }
    }

    // Causes the player to take damage
    private void TakeDamage(int damage) {
        // Set current health and check if the player has died
        currentHealth -= damage;
        if(currentHealth <= 0) {
            currentHealth = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the game
        }
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    // Causes the player to heal
    public void Heal(int healingAmount) {
        // Set current health and check if the player is at max health
        currentHealth += healingAmount;
        if(currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth, maxHealth);
    }
}
