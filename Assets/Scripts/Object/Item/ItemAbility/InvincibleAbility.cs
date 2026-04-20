using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "InvincibleAbility", menuName = "ItemAbility/InvincibleAbility")]
public class InvincibleAbility : ItemAbility
{
    public float Duration = 5f;
    
    public override void ApplyAbility(Player player)
    {
        player.StartCoroutine(InvincibleCoroutine(player));
    }

    private IEnumerator InvincibleCoroutine(Player player)
    {
        player.isInvincible = true;
        yield return new WaitForSeconds(Duration);
        player.isInvincible = false;
    }
}