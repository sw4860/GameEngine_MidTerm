using UnityEngine;

[CreateAssetMenu(fileName = "DashResetAbility", menuName = "ItemAbility/DashResetAbility")]
public class DashResetAbility : ItemAbility
{
    public override void ApplyAbility(Player player)
    {
        if (player != null)
        {
            player.lastDashTime = Time.time - player.dashCooldown;
        }
    }
}