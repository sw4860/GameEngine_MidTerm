using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionTrap : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float Interval = 0.25f;

    private GameObject triggerObject;
    private bool isTriggered = false;
    private float timer = 0f;

    void Update()
    {
        if (isTriggered)
        {
            timer += Time.deltaTime;
            if (timer >= Interval)
            {
                Health health = triggerObject.GetComponent<Health>();
                if (health != null)
                {
                    if (triggerObject.TryGetComponent(out Player player))
                        player.TakeDamage(damage);
                    else
                        health.TakeDamage(damage);
                }
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        triggerObject = collision.gameObject;
        timer = Interval;
        isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer = 0f;
        isTriggered = false;
    }
}
