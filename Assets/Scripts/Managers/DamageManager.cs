using UnityEngine;

public class DamageManager : Singleton<DamageManager>
{

    [Header("Config")]
    [SerializeField] private DamageText damageTextPrefab;

    // create instance of prefab in scene 
    public void ShowDamageText(float damageAmount, Transform parent) 
    {
        DamageText text = Instantiate(damageTextPrefab, parent);
        Debug.Log($"damage: {damageAmount}");
        text.transform.position = parent.position;
        text.transform.position += Vector3.right * 0.5f;
        text.SetDamageText(damageAmount);
    }

}
