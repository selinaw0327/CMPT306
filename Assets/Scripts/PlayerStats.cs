using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    // Variables needed for healthbar
    public StatBar healthBar;
    public int maxHealth;
    public int currentHealth;

    // Energy Bar
    public StatBar energyBar;
    public int maxEnergy;
    public int currentEnergy;

    // Hunger Bar
    public StatBar hungerBar;
    public int maxHunger;
    public int currentHunger;

    public int currentLevel;
    public int damage;
    
    public float energyDecreaseRate;
    private float energyNextTimeToDecrease = 0.0f;

    public float hungerDecreaseRate;
    private float hungerNextTimeToDecrease = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set current and max health, as well as position of healthbar
        currentHealth = maxHealth;
        healthBar.SetMaxStat(maxHealth);
        healthBar.transform.position = transform.position + healthBar.offset; // Places healthbar above players head based on its offset

        currentEnergy = maxEnergy;
        energyBar.SetMaxStat(maxEnergy);

        currentHunger = maxHunger;
        hungerBar.SetMaxStat(maxHunger);
    }

    // Update is called once per frame
    void Update()
    {
        // Timed system to decrease energy
        if(Time.time >= energyNextTimeToDecrease) {
            energyNextTimeToDecrease = Time.time + 1.0f / energyDecreaseRate;
            TakeEnergy(1);
        }
        // Timed system to decrease hunger
        if(Time.time >= hungerNextTimeToDecrease) {
            hungerNextTimeToDecrease = Time.time + 1.0f / hungerDecreaseRate;
            TakeHunger(1);
        }

        // Simple test to take damage
        if(Input.GetKeyDown(KeyCode.Space) && currentHealth > 0) {
            TakeDamage(10);
        }
        // Test to heal player
        if(Input.GetKeyDown(KeyCode.H) && currentHealth < maxHealth) {
            Heal(10);
        }
        // Test to take away energy
        if(Input.GetKeyDown(KeyCode.Y) && currentEnergy > 0) {
            TakeEnergy(10);
        }
        // Test to take away hunger
        if(Input.GetKeyDown(KeyCode.R) && currentHunger > 0) {
            TakeHunger(10);
        }
    }

    // Causes the player to take damage
    private void TakeDamage(int damage) {
        // Set current health and check if the player has died
        currentHealth -= damage;
        if(currentHealth <= 0) {
            currentHealth = 0;
            SceneManager.LoadScene("MainMenu"); // Restart the game
        }
        healthBar.SetStat(currentHealth, maxHealth);
    }

    // Causes the player to heal
    public void Heal(int healingAmount) {
        // Set current health and check if the player is at max health
        currentHealth += healingAmount;
        if(currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
        healthBar.SetStat(currentHealth, maxHealth);
    }

    public void TakeEnergy(int energyLoss) {
        currentEnergy -= energyLoss;
        if(currentEnergy <= 0) {
            currentEnergy = 0;
        }
        energyBar.SetStat(currentEnergy, maxEnergy);
    }

    public void TakeHunger(int hungerLoss) {
        currentHunger -= hungerLoss;
        if(currentHunger <= 0) {
            currentHunger = 0;
        }
        hungerBar.SetStat(currentHunger, maxHunger);
    }
}
