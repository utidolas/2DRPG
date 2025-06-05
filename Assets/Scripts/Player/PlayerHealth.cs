using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    // reference of Player Stats SO 
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private PlayerAnimations playerAnimations; // reference of script "PlayerAnimations.cs"
    public bool PlayerHasHealth = true;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>(); // reference of script "PlayerAnimations.cs"
    }

    private void Update()
    {
        if (stats.Health <= 0f){
            PlayerDead();
        }
    }

    public void TakeDamage(float amount)
    {
        if (stats.Health <= 0f) return;

        // taking damage
        stats.Health -= amount;
        DamageManager.Instance.ShowDamageText(amount, transform);

        // checking if player is alive
        if (stats.Health <= 0.0001f)
        {
            PlayerDead();
            PlayerHasHealth = false;
        }
    }

    public void RestoreHealth(float amount)
    {
        stats.Health += amount;
        // making sure dont go beyond Max Health
        if(stats.Health > stats.MaxHealth)
        {
            stats.Health = stats.MaxHealth;
        }
    }

    // check if health is not max and if player's not dead
    public bool CanRestoreHealth()
    {
        return stats.Health > 0 && stats.Health < stats.MaxHealth;
    }

    private void PlayerDead()
    {
        playerAnimations.SetDeadAnimation();
        Debug.Log("Dead");
    }

}
