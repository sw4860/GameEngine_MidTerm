using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHP = 10;
    protected float currentHealth;

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CreateDamageText(damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void CreateDamageText(float damage)
    {
        GameObject DmgGO = Instantiate(Resources.Load<GameObject>("Damage_Text"));
        DmgGO.GetComponent<DamageText>().Init(damage);
        DmgGO.transform.SetParent(transform);
        DmgGO.transform.position = new Vector2(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f), Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f));
    }

    public void CreateDamageText(float damage, Color newColor)
    {
        GameObject DmgGO = Instantiate(Resources.Load<GameObject>("Damage_Text"));
        DmgGO.GetComponent<DamageText>().Init(damage, newColor);
        DmgGO.transform.SetParent(transform);
        DmgGO.transform.position = new Vector2(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f), Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f));
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
