using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHP = 100;
    protected int currentHealth;

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}
