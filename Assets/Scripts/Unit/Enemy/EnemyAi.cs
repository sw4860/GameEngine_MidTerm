using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private EnemyAiLogic enemyAiLogic;
    [HideInInspector] public Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (enemyAiLogic == null) return;

        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance < enemyAiLogic.detectionRange)
        {
            enemyAiLogic.MoveTowardsPlayer(this);
            if (playerDistance < enemyAiLogic.attackRange)
            {
                enemyAiLogic.Attack(this);
            }
        }
    }
}
