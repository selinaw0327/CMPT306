using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    // Variables needed for healthbar
    public StatBar healthBar;
    public int initialHealth;
    public int currentHealth;
    public int additionalHealth;
    public int overallHealth;

    public int currentLevel = 0;
    public int initialDamage;
    public int additionalDamage;
    public int overallDamage;

    public float energyDecreaseRate;
    private float energyNextTimeToDecrease = 0.0f;

    public float hungerDecreaseRate;
    private float hungerNextTimeToDecrease = 0.0f;

    public bool swordEquipped;
    public string sword = "none";

    public GameObject deathScreenUI;

    public GameObject plyerDeath;

    public bool invincible;

    // Start is called before the first frame update
    void Start() {
        swordEquipped = false;
        invincible = false;
        switch (MenuFunctions.character) {
            case 1:
                SetAllStats(150, 0);
                break;
            case 2:
                SetAllStats(100, 15);
                break;
            default:
                break;
        }
    }
    
    // Update is called once per frame
    void Update() {
    }

    private void SetAllStats(int newMaxHealth, int newMaxDamage) {
        initialHealth = newMaxHealth;

        SetOverallHealth();

        currentHealth = overallHealth;
        healthBar.SetMaxStat(overallHealth);
        healthBar.transform.position = transform.position + healthBar.offset; // Places healthbar above players head based on its offset

        initialDamage = newMaxDamage;
        SetOverallDamage();
    }

    // Causes the player to take damage
    public void TakeDamage(int damage) {
        // Set current health and check if the player has died
        currentHealth -= damage;
        if(currentHealth <= 0) {
            currentHealth = 0;
            deathScreenUI.SetActive(true);
            plyerDeath.GetComponent<AudioSource>().Play();
        }
        healthBar.SetStat(currentHealth, overallHealth);
    }

    // Causes the player to heal
    public void Heal(int healingAmount) {
        // Set current health and check if the player is at max health
        currentHealth += healingAmount;
        if(currentHealth >= overallHealth) {
            currentHealth = overallHealth;
        }
        healthBar.SetStat(currentHealth, overallHealth);
    }

    public void SetAdditionalHealth(int i)
    {
        additionalHealth = i;
        SetOverallHealth();
        currentHealth = overallHealth;
        healthBar.SetMaxStat(overallHealth);
    }

    public void SetOverallHealth()
    {
        overallHealth = initialHealth + additionalHealth;
    }

    public void SetAdditionalDamage(int i)
    {
        additionalDamage = i;
        SetOverallDamage();
    }

    public void SetOverallDamage()
    {
        overallDamage = initialDamage + additionalDamage;
    }

    public void StartInvincibility()
    {
        StartCoroutine(InvinsibleCo());
    }

    IEnumerator InvinsibleCo()
    {
        invincible = true;
        yield return new WaitForSeconds(0.75f); // amount of time it takes for another enenmy to be able to attack
        invincible = false;
    }
}
