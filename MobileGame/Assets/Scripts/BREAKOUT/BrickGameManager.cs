using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform brickParent;

    void Update()
    {
        if (brickParent.childCount == 0)
        {
            Debug.Log("You Win!");
            // Reload scene or show win screen
        }
    }
}
