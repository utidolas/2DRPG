using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Config")]
    public int Level;

    [Header("Health")]
    public float Health;
    public float MaxHealth;

    [Header("Mana")]
    public float Mana;
    public float MaxMana;

    [Header("Experience")]
    public float CurrentExp;
    public float NextLevelExp;
    public float InitialNextLevelExp;
    [Range(1f, 100f)]public float ExpMultipliyer;
    public void ResetPlayer()
    {
        Health = MaxHealth;
        Mana = MaxMana;
        Level = 1;
        CurrentExp = 0;
        NextLevelExp = InitialNextLevelExp;
    }
}
