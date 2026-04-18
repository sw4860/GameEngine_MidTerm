using UnityEngine;

public class ItemAbility : ScriptableObject
{
    public Sprite ItemSprite;
    
    public virtual void ApplyAbility(Player player) { }
}