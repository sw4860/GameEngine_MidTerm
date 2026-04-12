using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHP;
    }

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
