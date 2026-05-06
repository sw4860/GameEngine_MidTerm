using UnityEngine;

[CreateAssetMenu(fileName = "GravityChangeEnable", menuName = "ItemAbility/GravityChangeEnable")]
public class GravityChangeEnable : ItemAbility
{
    public override void ApplyAbility(Player player)
    {
        base.ApplyAbility(player);
        StaticValues.CanGravityChange = true;
    }
}