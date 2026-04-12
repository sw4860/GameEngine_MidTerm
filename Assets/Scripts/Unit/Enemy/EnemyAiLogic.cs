using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAiLogic", menuName = "ScriptableObjects/EnemyAiLogic", order = 1)]
public class EnemyAiLogic : ScriptableObject
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;

    public void MoveTowardsPlayer(EnemyAi enemy)
    {
        Vector2 direction = (enemy.player.position - enemy.transform.position).normalized;
        enemy.transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    public void Attack(EnemyAi enemy)
    {
        
    }
}
