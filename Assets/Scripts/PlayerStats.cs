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

    public int currentLevel = 0;
    public int damage;
    
    public float energyDecreaseRate;
    private float energyNextTimeToDecrease = 0.0f;

    public float hungerDecreaseRate;
    private float hungerNextTimeToDecrease = 0.0f;

    public bool swordEquipped;

    // Start is called before the first frame update
    void Start() {
        swordEquipped = false;
        switch (MenuFunctions.character) {
            case 1:
                SetAllStats(100, 150, 100);
                break;
            case 2:
                SetAllStats(150, 50, 150);
                break;
            case 3:
                SetAllStats(50, 200, 50);
                break;
        }
    }
    
    // Update is called once per frame
    void Update() {
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
    }

    private void SetAllStats(int newMaxHealth, int newMaxEnergy, int newMaxHunger) {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        healthBar.SetMaxStat(maxHealth);
        healthBar.transform.position = transform.position + healthBar.offset; // Places healthbar above players head based on its offset

        // Set current and max energy
        maxEnergy = newMaxEnergy;
        currentEnergy = maxEnergy;
        energyBar.SetMaxStat(maxEnergy);

        // Set current and amx hunger
        maxHunger = newMaxHunger;
        currentHunger = maxHunger;
        hungerBar.SetMaxStat(maxHunger);
    }

    // Causes the player to take damage
    public void TakeDamage(int damage) {
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

    // Causes the player to lose energy
    public void TakeEnergy(int energyLoss) {
        currentEnergy -= energyLoss;
        if(currentEnergy <= 0) {
            currentEnergy = 0;
        }
        energyBar.SetStat(currentEnergy, maxEnergy);
    }

    // Causes the player to lose hunger
    public void TakeHunger(int hungerLoss) {
        currentHunger -= hungerLoss;
        if(currentHunger <= 0) {
            currentHunger = 0;
        }
        hungerBar.SetStat(currentHunger, maxHunger);
    }
}
