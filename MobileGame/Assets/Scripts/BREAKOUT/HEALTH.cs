using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar; // Health bar reference
    [SerializeField] private int maxHealth = 3; // Maximum player health
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void DrainHealth()
    {
        currentHealth--;

        if (healthBar != null)
            healthBar.value = currentHealth;

        Debug.Log("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0; // Freeze the game
    }


    public void ResetHealth()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
        Debug.Log("Player health has been reset.");
    }
}
