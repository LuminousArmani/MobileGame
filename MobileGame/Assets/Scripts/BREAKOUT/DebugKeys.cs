using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKeys : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;   // Reference to PlayerHealth
    [SerializeField] private GameObject winPanel;         // Reference to the Win UI Panel
    [SerializeField] private UnityEngine.UI.Text winText; // Reference to the Win Text UI
    [SerializeField] private Rigidbody2D ballRigidbody;   // Reference to the ball's Rigidbody2D

    private bool slowMotionEnabled = false; // Toggle for slow-motion debugging
    private bool isGameOver = false;        // Prevents duplicate calls to win/lose

    void Update()
    {
        // Auto-Win (Key: W)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Debug Key: Auto-Win triggered");
            TriggerWin("Debug");
        }

        // Auto-Lose (Key: L)
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Debug Key: Auto-Lose triggered");
            TriggerLose();
        }

        // Reset Health (Key: H)
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Debug Key: Reset Health triggered");
            if (playerHealth != null)
                playerHealth.ResetHealth();
        }

        // Toggle Slow Motion (Key: S)
        if (Input.GetKeyDown(KeyCode.S))
        {
            slowMotionEnabled = !slowMotionEnabled;
            Time.timeScale = slowMotionEnabled ? 0.5f : 1f; // Slow down to 0.5x or reset to normal speed
            Debug.Log("Debug Key: Slow Motion " + (slowMotionEnabled ? "Enabled" : "Disabled"));
        }

        // Speed Up Ball (Key: B)
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Debug Key: Speed Up Ball triggered");
            if (ballRigidbody != null)
            {
                ballRigidbody.linearVelocity *= 1.5f; // Increase ball speed by 50%
            }
        }

        // Restart Scene (Key: R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Debug Key: Restart Scene triggered");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Quit Application (Key: Q)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Debug Key: Quit Application triggered");
            Application.Quit();
        }
    }

    // Auto-Win logic
    private void TriggerWin(string winner)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0f; // Freeze the game
            winPanel.SetActive(true); // Show the win UI
            if (winText != null)
                winText.text = winner + " Wins!";
        }
    }

    // Auto-Lose logic
    private void TriggerLose()
    {
        if (playerHealth != null)
        {
            while (playerHealth.currentHealth > 0)
                playerHealth.DrainHealth();
        }
    }
}
