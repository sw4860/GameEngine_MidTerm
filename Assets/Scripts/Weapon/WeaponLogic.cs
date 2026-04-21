using UnityEngine;

public class WeaponLogic : ScriptableObject
{
    public string Name;
    public GameObject projectile;
    public float Damage;
    public float attackCooldown;
    public float Speed;
    public float Duration = 3f;

    public virtual void Attack(Transform transform, Vector2 targetPosition) {}
}
