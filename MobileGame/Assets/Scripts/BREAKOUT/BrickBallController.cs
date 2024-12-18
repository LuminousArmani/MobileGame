using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float ballSpeed = 10f; // Serialized field for ball speed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(ballSpeed, ballSpeed); // Set initial velocity
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for collision with bricks
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject); // Destroy the brick
            CheckWinCondition(); // Check if all bricks are destroyed
        }
        else if (collision.gameObject.CompareTag("BottomWall"))
        {
            FindObjectOfType<PlayerHealth>().DrainHealth(); // Drain health
            ResetBallPosition(); // Reset ball position
        }
    }

    private void ResetBallPosition()
    {
        rb.linearVelocity = Vector2.zero; // Stop the ball
        transform.position = new Vector2(0, -3); // Reset to starting position
        rb.linearVelocity = new Vector2(ballSpeed, ballSpeed); // Reset velocity
    }

    private void CheckWinCondition()
    {
        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0) // Check for bricks
        {
            FindObjectOfType<WinManager>().GameOver("Player");
        }
    }
}
