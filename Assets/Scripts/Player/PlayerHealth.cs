using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    // reference of Player Stats SO 
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private PlayerAnimations playerAnimations; // reference of script "PlayerAnimations.cs"

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>(); // reference of script "PlayerAnimations.cs"
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)){
            TakeDamage(15f);
        }
    }

    public void TakeDamage(float amount)
    {
        // taking damage
        stats.Health -= amount;

        // checking if player is alive
        if (stats.Health <= 0f)
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        playerAnimations.SetDeadAnimation();
        Debug.Log("Dead");
    }

}
