using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedBoostAbility", menuName = "ItemAbility/SpeedBoostAbility")]
public class SpeedBoostAbility : ItemAbility
{
    public float Effect = 1f;
    public float Duration = 5f;
    
    public override void ApplyAbility(Player player)
    {
        player.StartCoroutine(SpeedBoostCoroutine(player));
    }

    private IEnumerator SpeedBoostCoroutine(Player player)
    {
        player.moveSpeed += Effect;
        yield return new WaitForSeconds(Duration);
        player.moveSpeed -= Effect;
    }
}