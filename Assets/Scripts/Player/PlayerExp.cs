using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public void AddExp(float amount)
    {
        stats.CurrentExp += amount;
        // check if level up, reset exp and update it
        while (stats.CurrentExp >= stats.NextLevelExp)
        {
            stats.CurrentExp -= stats.NextLevelExp;
            UpdateExp();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) 
        {
            AddExp(300f);
        }
    }

    private void UpdateExp()
    {
        stats.Level++;
        float currentExpRequired = stats.NextLevelExp;
        float newNextLevelExp = Mathf.Round(currentExpRequired + stats.NextLevelExp * (stats.ExpMultipliyer / 100f)); // sum current level + a percentage 
        stats.NextLevelExp = newNextLevelExp;
    }
}
