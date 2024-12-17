using UnityEngine;
using UnityEngine.SceneManagement; // For scene loading

public class MenuButton : MonoBehaviour
{
    public void OnMenuButtonClick()
    {
        // Load the main menu scene (make sure the menu scene is added in Build Settings)
        SceneManager.LoadScene("TitleScreen"); // Replace with your main menu scene name
    }

    public void OnRestartButtonClick()
    {
        // Reload the current level/scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButtonClick()
    {
        // Exit the application
        Application.Quit();
    }
}
