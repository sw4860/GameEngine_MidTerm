using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpBoostAbility", menuName = "ItemAbility/JumpBoostAbility")]
public class JumpBoostAbility : ItemAbility
{
    public float Effect = 1f;
    public float Duration = 5f;
    
    public override void ApplyAbility(Player player)
    {
        player.StartCoroutine(JumpBoostCoroutine(player));
    }

    private IEnumerator JumpBoostCoroutine(Player player)
    {
        player.jumpPower += Effect;
        yield return new WaitForSeconds(Duration);
        player.jumpPower -= Effect;
    }
}