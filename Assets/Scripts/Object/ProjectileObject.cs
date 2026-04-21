using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public Vector2 Direction;
    public float Duration = 3f;

    private Rigidbody2D rb;


    public void Init(float damage, float speed, float duration, Vector2 direction)
    {
        Damage = damage;
        Speed = speed;
        Duration = duration;
        Direction = direction.normalized;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = Direction * Speed;
        Duration -= Time.deltaTime;
        if (Duration <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Wall")) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out Player player))
                player.TakeDamage(Damage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
