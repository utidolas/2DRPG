using UnityEngine;

public class DamageManager : MonoBehaviour
{
    // creating Singleton to access public methods/vars
    public static DamageManager Instance;

    [Header("Config")]
    [SerializeField] private DamageText damageTextPrefab;

    private void Awake()
    {
        Instance = this;
    }

    // create instance of prefab in scene 
    public void ShowDamageText(float damageAmount, Transform parent) 
    {
        DamageText text = Instantiate(damageTextPrefab, parent);
        text.transform.position += Vector3.right * 0.5f;
        text.SetDamageText(damageAmount);
    }

}
