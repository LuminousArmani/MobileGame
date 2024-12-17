using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attriutes")]
    [SerializeField] private float rocketSpeed = 5f;
    [SerializeField] private int rocketDamage = 1;


    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
        if (target != null)
        {
            
            RotateTowards(initialDirection);
        }
    }

    private void FixedUpdate()
    {

        if (!target) return;
        Vector2 direction = (target.position - transform.position);

        rb.linearVelocity = direction * rocketSpeed;

    }
    private void RotateTowards(Vector2 direction)
    {
        // Calculate the angle to rotate towards
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(rocketDamage);
        Destroy(gameObject);
    }
}
