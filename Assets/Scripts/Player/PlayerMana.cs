using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(15f);
        }
    }

    public void UseMana(float amount)
    {
        stats.Mana = Mathf.Max(stats.Mana -= amount, 0f); // 'check' if player has enough mana to cast, otherwhise set to 0
    }
}
