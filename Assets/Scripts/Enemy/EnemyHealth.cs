using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [SerializeField] private float health;

    public float CurrentHealth { get; private set; }

    // ref of scripts/components
    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();   
        enemySelector = GetComponent<EnemySelector>();
    }

    private void Start()
    {
        CurrentHealth = health;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0f)
        {
            animator.SetTrigger("Dead");
            enemyBrain.enabled = false;
            enemySelector.NoSelectionCallback();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }

}
