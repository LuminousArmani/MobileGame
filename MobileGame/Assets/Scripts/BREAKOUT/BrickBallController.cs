using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float ballSpeed = 5f; // Initial ball speed
    private Rigidbody2D rb;
    private Vector2 _ballDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    private void FixedUpdate()
    {
        // Prevent the ball from getting stuck if it's moving too slowly
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * ballSpeed; // Reset to a reasonable speed
        }

        // Apply small random shake to prevent the ball from getting stuck in one spot
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            rb.linearVelocity += new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)); // Small random velocity adjustment
        }
    }

    private void LaunchBall()
    {
        // Randomly set the initial direction of the ball
        _ballDirection = new Vector2(Random.Range(-1f, 1f), 1).normalized;
        rb.linearVelocity = _ballDirection * ballSpeed;
    }

    // Ball collision logic
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the ball collides with the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Apply a small random variation to the horizontal direction when hitting walls
            _ballDirection.x += Random.Range(-0.2f, 0.2f); // Small random adjustment to horizontal direction
            _ballDirection.y = Mathf.Sign(_ballDirection.y) * Mathf.Abs(_ballDirection.y); // Ensure the vertical direction stays consistent
            _ballDirection = _ballDirection.normalized * ballSpeed;
            rb.linearVelocity = _ballDirection;
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            // Apply random angle adjustment when hitting a paddle
            // Reverse the horizontal direction when hitting the paddle
            _ballDirection.x = -_ballDirection.x;

            // Adjust vertical angle based on where the ball hit the paddle
            float paddleHitFactor = (transform.position.y - collision.transform.position.y) / collision.collider.bounds.size.y;
            _ballDirection.y = Mathf.Sign(_ballDirection.y) * paddleHitFactor;

            // Normalize and apply the ball speed
            _ballDirection = _ballDirection.normalized * ballSpeed;
            rb.linearVelocity = _ballDirection;
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            // When the ball collides with a brick, destroy the brick
            Destroy(collision.gameObject); // Destroy the brick object
        }
    }
}
