using UnityEngine;

[CreateAssetMenu(fileName = "DashResetAbility", menuName = "ItemAbility/DashResetAbility")]
public class DashResetAbility : ItemAbility
{
    public override void ApplyAbility(Player player)
    {
        if (player != null)
        {
            base.ApplyAbility(player);
            player.lastDashTime = Time.time - player.dashCooldown;
        }
    }
}