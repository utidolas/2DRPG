using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public void AddExp(float amount)
    {
        stats.TotalExp += amount;
        stats.CurrentExp += amount;
        // check if level up, reset exp and update it
        while (stats.CurrentExp >= stats.NextLevelExp)
        {
            stats.CurrentExp -= stats.NextLevelExp;
            NextLevel();
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) 
        {
            AddExp(300f);
        }
    }

    private void NextLevel()
    {
        stats.Level++;
        stats.AttributePoints++;
        float currentExpRequired = stats.NextLevelExp;
        float newNextLevelExp = Mathf.Round(currentExpRequired + stats.NextLevelExp * (stats.ExpMultipliyer / 100f)); // sum current level + a percentage 
        stats.NextLevelExp = newNextLevelExp;
    }
}
