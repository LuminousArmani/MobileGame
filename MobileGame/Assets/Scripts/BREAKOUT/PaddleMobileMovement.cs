using UnityEngine;

public class PaddleMobile : MonoBehaviour
{
    public float speed = 10f; // Speed of paddle movement
    public float boundary = 8f; // Restrict paddle movement within screen bounds

    private float moveDirection = 0f; // Direction of paddle movement (-1 for left, 1 for right, 0 for no movement)

    void Update()
    {
        // Move the paddle based on the direction
        float move = moveDirection * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        // Restrict paddle movement within screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, -boundary, boundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    // Methods to control paddle movement
    public void MoveLeft()
    {
        moveDirection = -1f; // Move left
    }

    public void MoveRight()
    {
        moveDirection = 1f; // Move right
    }

    public void StopMovement()
    {
        moveDirection = 0f; // Stop moving
    }
}
