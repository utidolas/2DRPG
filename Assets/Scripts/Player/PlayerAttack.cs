using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private EnemyBrain enemyTarget;
    private Coroutine atttackCoroutine;

    private void Awake()
    {
        actions = new PlayerActions();
        playerAnimations = GetComponent<PlayerAnimations>();

    }

    private void Start()
    {
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Attack()
    {

    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
