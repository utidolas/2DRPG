using UnityEngine;

public class ActionAttack : FSMAction
{
    [Header("Config")]
    [SerializeField] private float damage;
    [SerializeField] private float timeBtwAttacks;

    private EnemyBrain enemyBrain;
    private float timer;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override void Act()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        if (enemyBrain.Player == null) return;
        timer -= Time.deltaTime;
        if (timer < 0f) 
        {
            PlayerHealth playerHealth = enemyBrain.Player.GetComponent<PlayerHealth>();
            if (playerHealth.PlayerHasHealth == false) return;
            playerHealth.TakeDamage(damage);
            timer = timeBtwAttacks;
        }

    }

}
