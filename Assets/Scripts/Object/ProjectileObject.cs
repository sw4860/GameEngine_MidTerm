using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    public float Damage;
    public float Speed;
    public Vector2 Direction;
    public float Duration = 3f;
    public bool IsEnemy;

    private Rigidbody2D rb;


    public void Init(float damage, float speed, float duration, Vector2 direction, bool isEnemy = true)
    {
        Damage = damage;
        Speed = speed;
        Duration = duration;
        Direction = direction.normalized;
        IsEnemy = isEnemy;
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
        if (collision.gameObject.CompareTag("Ground")) Destroy(gameObject);

        if (IsEnemy)
        {
            if (collision.gameObject.CompareTag("Enemy")) return;
            
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
        else if (!IsEnemy)
        {
            if (collision.gameObject.CompareTag("Player")) return;

            if (collision.gameObject.TryGetComponent(out Health enemy))
            {
                enemy.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}
