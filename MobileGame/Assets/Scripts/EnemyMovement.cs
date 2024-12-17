using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float movespeed = 2f;
    [SerializeField] private int damage = 1;//damage to player

    private Transform target;
    private int pathIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = TDLevelManager.main.path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)//if the enemy reaches the end of the array in TDLevelManager
        {
            pathIndex++;

            if (pathIndex >= TDLevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();//lower currnet enemies alive

                TDLevelManager.main.Health -= damage;//player takes damage

                Destroy(gameObject);
                return;
            }
            else
            {
                target = TDLevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;// takes the array and goes to the fisrt one in the list and slowly goes to each one in order

        rb.linearVelocity = direction * movespeed;//keeps same move speed
    }
}
