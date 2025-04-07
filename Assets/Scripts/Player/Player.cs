using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; // reference of script "PlayerStats.cs"

    public PlayerStats Stats => stats; // creating property to return private var
}
