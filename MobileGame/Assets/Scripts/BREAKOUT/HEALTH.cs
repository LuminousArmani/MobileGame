using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Health variables
    public int maxHealth = 100;
    private int currentHealth;

    // UI references
    private Slider healthBar;   // Single declaration, no ambiguity
    public Text healthText;     // Optional: Reference for displaying health text

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Find health bar in the scene
        healthBar = Object.FindFirstObjectByType<Slider>(); // New replacement for FindObjectOfType
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        // Update UI initially
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    // Method to reduce health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevent health from going below 0
        UpdateHealthUI();

        // Check if health reaches 0
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    // Handle player death
    private void HandleDeath()
    {
        Debug.Log("Player is dead!");
        // You can add functionality like showing game over UI or restarting the game here
    }

    // Example: Detect collisions with the bottom wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BottomWall")) // Ensure BottomWall has this tag
        {
            TakeDamage(10); // Reduce health by 10 when the ball hits the bottom wall
        }
    }
}
