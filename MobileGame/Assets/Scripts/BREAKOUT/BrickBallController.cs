using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(3, 3); // Set an initial velocity
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for collision with bricks
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject); // Destroy the brick
        }
        else if (collision.gameObject.CompareTag("BottomWall"))
        {
            FindObjectOfType<PlayerHealth>().DrainHealth(); // Drain health
            ResetBallPosition(); // Reset ball position
        }
    }

    private void ResetBallPosition()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = new Vector2(0, -3); // Adjust to your start position
        rb.linearVelocity = new Vector2(3, 3); // Reset velocity
    }
}
