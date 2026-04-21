using UnityEngine;

[CreateAssetMenu(fileName = "Pistol", menuName = "Weapons/Pistol")]
public class Pistol : WeaponLogic
{
    public override void Attack(Transform player, Vector2 targetPosition)
    {
        GameObject GO = Instantiate(projectile, player.position, Quaternion.identity);
        GO.GetComponent<ProjectileObject>().Init(Damage, Speed, Duration,
                                                 targetPosition - new Vector2(player.position.x, player.position.y), false);
    }
}