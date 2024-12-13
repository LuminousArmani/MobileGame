using UnityEngine;

public class PongGameController : MonoBehaviour
{
    public Rigidbody2D ball;
    public float ballSpeed = 5f;

    public Transform leftPaddle, rightPaddle;
    public float paddleSpeed = 10f;
    public float aiReactionSpeed = 0.1f; // AI reaction speed

    private Vector2 _ballDirection;

    void Start()
    {
        ResetBall();
    }

    void Update()
    {
        // Player 1 (Human) Movement
        float leftMove = Input.GetAxis("Vertical");
        Vector3 newLeftPosition = leftPaddle.position + Vector3.up * leftMove * paddleSpeed * Time.deltaTime;
        newLeftPosition.y = Mathf.Clamp(newLeftPosition.y, -4.5f, 4.5f); // Adjust bounds to fit your scene
        leftPaddle.position = newLeftPosition;

        // Player 2 (AI) Movement
        Vector2 targetPosition = new Vector2(rightPaddle.position.x, Mathf.Clamp(ball.position.y, -4.5f, 4.5f));
        rightPaddle.position = Vector2.MoveTowards(rightPaddle.position, targetPosition, aiReactionSpeed * paddleSpeed * Time.deltaTime);
    }

    void ResetBall()
    {
        ball.position = Vector2.zero;
        _ballDirection = new Vector2(Random.value > 0.5f ? 1 : -1, Random.Range(-1f, 1f)).normalized;
        ball.linearVelocity = _ballDirection * ballSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Calculate bounce direction based on where the ball hit the paddle
            float hitPoint = ball.position.y - collision.transform.position.y;
            float paddleHeight = collision.collider.bounds.size.y;

            float bounceAngle = hitPoint / paddleHeight; // Normalize bounce angle
            _ballDirection = new Vector2(-_ballDirection.x, bounceAngle).normalized;
            ball.linearVelocity = _ballDirection * ballSpeed;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Reverse vertical direction when hitting walls
            _ballDirection = new Vector2(_ballDirection.x, -_ballDirection.y).normalized;
            ball.linearVelocity = _ballDirection * ballSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}");

        if (collision.CompareTag("LeftGoal"))
        {
            Debug.Log("Player 2 Scores!");
            ResetBall();
        }
        else if (collision.CompareTag("RightGoal"))
        {
            Debug.Log("Player 1 Scores!");
            ResetBall();
        }
    }
}
