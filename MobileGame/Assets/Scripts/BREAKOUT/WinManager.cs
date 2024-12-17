using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI components

public class WinManager : MonoBehaviour
{
    public GameObject winPanel; // Reference to the WinPanel
    public Text winText;        // Reference to the Win text
    public Button playAgainButton; // Button for play again
    public Button goBackButton;   // Button for go back to menu

    private bool isGameOver = false; // To track if the game is over

    // Call this method when the game is won (e.g., player scores)
    public void GameOver(string winner)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0f; // Freeze the game
            winPanel.SetActive(true); // Show the win UI
            winText.text = winner + " Wins!"; // Set winner text
        }
    }

    // Play Again button functionality
    public void PlayAgain()
    {
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Go Back to Menu button functionality
    public void GoBackToMenu()
    {
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene("MainMenu"); // Load your main menu scene (replace with your actual menu scene name)
    }

    private void Start()
    {
        // Add listeners to the buttons
        playAgainButton.onClick.AddListener(PlayAgain);
        goBackButton.onClick.AddListener(GoBackToMenu);
    }
}
