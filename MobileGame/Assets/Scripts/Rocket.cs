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
    }

    private void FixedUpdate()
    {

        if (!target) return;
        Vector2 direction = (target.position - transform.position);

        rb.linearVelocity = direction * rocketSpeed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().TakeDamage(rocketDamage);
        Destroy(gameObject);
    }
}
