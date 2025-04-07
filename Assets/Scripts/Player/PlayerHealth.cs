using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    // reference of Player Stats SO 
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

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
        Debug.Log("Dead");
    }

}
