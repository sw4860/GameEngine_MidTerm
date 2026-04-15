using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionTrap : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if (health != null)
        {
            if (collision.TryGetComponent(out Player player))
                player.TakeDamage(damage);
            else
                health.TakeDamage(damage);
        }
    }
}
