using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attriutes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    

    private Transform target;
    private Vector2 initialDirection;

    public void SetTarget(Transform _target)
    {
        target = _target;

        if (target != null)
        {
            initialDirection = (target.position - transform.position).normalized; // Store normalized direction
            RotateTowards(initialDirection);
        }
    }

    private void FixedUpdate()
    {

        if (target == null) return;

        // Move the bullet in the stored direction
        rb.linearVelocity = initialDirection * bulletSpeed;
    }
    private void RotateTowards(Vector2 direction)
    {
        // Calculate the angle to rotate towards
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}
