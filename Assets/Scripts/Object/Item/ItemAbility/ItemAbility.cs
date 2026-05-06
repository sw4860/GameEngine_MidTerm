using UnityEngine;

public class ItemAbility : ScriptableObject
{
    public Sprite ItemSprite;
    public int Score;
    
    public virtual void ApplyAbility(Player player) { RankPage.Score += Score; }
}