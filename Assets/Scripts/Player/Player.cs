using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; // reference of script "PlayerStats.cs"

    private PlayerAnimations animations; // reference of script "PlayerAnimations.cs"
    public PlayerStats Stats => stats; // creating property to return private var

    private void Awake()
    {
        animations = GetComponent<PlayerAnimations>();
    }

    public void ResetPlayer()
    {
        stats.ResetPlayer();
        // Reset animation
        animations.ResetPlayerAnimation();
    }
}
