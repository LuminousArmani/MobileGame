using UnityEngine;
using UnityEngine.SceneManagement; // To load the Pong scene

public class KonamiCodeDetector : MonoBehaviour
{
    // Konami code sequence
    private readonly KeyCode[] _konamiCode = {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A
    };

    private int _index = 0;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            // Check if the current key pressed matches the current step in the sequence
            if (Input.GetKeyDown(_konamiCode[_index]))
            {
                _index++;
                if (_index == _konamiCode.Length)
                {
                    Debug.Log("Konami Code Entered! Unlocking Pong...");
                    UnlockPong();
                    _index = 0; // Reset for next input
                }
            }
            else
            {
                _index = 0; // Reset if the sequence is broken
            }
        }
    }

    public void UnlockPong()
    {
        // Load the Pong scene
        SceneManager.LoadScene("Pong");
    }
}
