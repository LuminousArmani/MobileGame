using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    public float boundary = 8f;

    void Update()
    {
        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(move, 0, 0);

        // Restrict paddle movement within screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, -boundary, boundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
