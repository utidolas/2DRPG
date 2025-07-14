using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public float CurrentMana {  get; private set; }

    private void Start()
    {
        ResetMana();
    }

    private void Update()
    {
        /* Debugging purposes
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(15f);
        }
        */
    }

    public void UseMana(float amount)
    {
        stats.Mana = Mathf.Max(stats.Mana -= amount, 0f); // 'check' if player has enough mana to cast, otherwhise set to 0
        CurrentMana = stats.Mana;
    }

    public void RecoverMana(float amount)
    {
        stats.Mana += amount;
        stats.Mana = Mathf.Min(stats.Mana, stats.MaxMana); // making sure the mana dont go beyond maxmana by getting the minimun value between mana and Max Mana
    }

    public bool CanRecoverMana()
    {
        return stats.Mana > 0 && stats.Mana < stats.MaxMana;
 
    }

    public void ResetMana()
    {
        CurrentMana = stats.MaxMana;
    }
}
