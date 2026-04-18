using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHP = 10;
    protected float currentHealth;

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
