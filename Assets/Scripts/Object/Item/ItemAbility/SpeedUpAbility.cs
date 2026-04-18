using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpAbility", menuName = "ItemAbility/SpeedUpAbility")]
public class SpeedUpAbility : ItemAbility
{
    public float Effect = 1f;
    
    public override void ApplyAbility(Player player)
    {
        player.moveSpeed += Effect;
    }
}