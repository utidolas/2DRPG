using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; // reference of script "PlayerStats.cs"

    [Header("Test")]
    public ItemHealthPotion HealthPotion;
    public ItemManaPotion ManaPotion;

    private PlayerAnimations animations; // reference of script "PlayerAnimations.cs"
    public PlayerMana PlayerMana { get; private set; } // prop of players mana
    public PlayerHealth PlayerHealth { get; private set; } // prop of players health
    public PlayerStats Stats => stats; // creating property to return private var

    private void Awake()
    {
        PlayerMana = GetComponent<PlayerMana>();
        PlayerHealth = GetComponent<PlayerHealth>();
        animations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(HealthPotion.UseItem())
            {
                Debug.Log("Using Health Potion");
            }

            if (ManaPotion.UseItem())
            {
                Debug.Log("Using Mana Potion");
            }
        }
    }

    public void ResetPlayer()
    {
        stats.ResetPlayer();
        // Reset animation
        animations.ResetPlayerAnimation();
        // Reset mana
        PlayerMana.ResetMana();
    }
}
