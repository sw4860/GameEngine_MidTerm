using UnityEngine;

[CreateAssetMenu(fileName = "WeaponEnable", menuName = "ItemAbility/WeaponEnable")]
public class WeaponEnable : ItemAbility
{
    public override void ApplyAbility(Player player)
    {
        base.ApplyAbility(player);
        StaticValues.CanAttack = true;
    }
}