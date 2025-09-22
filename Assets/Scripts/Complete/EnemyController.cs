using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform target;

    private Rigidbody2D _rigidbody2D;

    
    private void Start()
    {
        target = GameObject.Find("Player").transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed);
        _rigidbody2D.linearVelocity = PlayerDirection() * moveSpeed;
    }

    private Vector2 PlayerDirection()
    {
        var heading = target.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; // This is now the normalized direction.
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
