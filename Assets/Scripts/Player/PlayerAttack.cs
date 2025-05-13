using UnityEngine;
using System.Collections;
using NUnit.Framework.Constraints;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform[] attackPositions; // array to check where attack will instantiate

    [Header("Melee Config")]
    [SerializeField] private ParticleSystem slashFX;
    [SerializeField] private float minDistanceMeleeAttack;

    public Weapon CurrentWeapon { get; set; }

    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerMana playerMana;
    private EnemyBrain enemyTarget;
    private Coroutine atttackCoroutine;

    // store position and rot we'll use for projectiles
    private Transform currentAttackPosition;
    private float currentAttackRotation;

    private void Awake()
    {
        actions = new PlayerActions();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMana = GetComponent<PlayerMana>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Start()
    {
        CurrentWeapon = initialWeapon;
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        GetFirePosition();
    }

    private void Attack()
    {
        if (enemyTarget == null) return;
        // if running coroutine stop 
        if(atttackCoroutine != null)
        {
            StopCoroutine(atttackCoroutine);
        }

        StartCoroutine(IEAttack());
    }

    private IEnumerator IEAttack()
    {
        if (currentAttackPosition == null) yield break;

        // check whether weapon is melee or magic and attack
        if(CurrentWeapon.WeaponType == WeaponType.Magic)
        {
            if (playerMana.CurrentMana < CurrentWeapon.RequiredMana) yield break; // check if has mana
            MagicAttack();
        }
        else
        {
            MeleeAttack();
        }



        playerAnimations.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackAnimation(false);
    }

    private void MagicAttack()
    {
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation)); // rotating proj
        Projectile projectile = Instantiate(CurrentWeapon.ProjectilePrefab, currentAttackPosition.position, rotation);
        projectile.Direction = Vector3.up; // moving the proj
        projectile.Damage = CurrentWeapon.Damage;
        playerMana.UseMana(CurrentWeapon.RequiredMana); // consume mana
    }

    private void MeleeAttack() 
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();
        float currentDistanceToEnemy = Vector3.Distance(enemyTarget.transform.position, transform.position);
        if(currentDistanceToEnemy <= minDistanceMeleeAttack)
        {
            enemyTarget.GetComponent<IDamageable>().TakeDamage(1f);
        }
    }

    private void GetFirePosition()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;
        switch (moveDirection.x)
        {
            case > 0f: // right
                currentAttackPosition = attackPositions[1];
                currentAttackRotation = -90f;
                break;

            case < 0f: // left
                currentAttackPosition = attackPositions[3];
                currentAttackRotation = -270f;
                break;
        }

        switch (moveDirection.y)
        {
            case > 0f: // up
                currentAttackPosition = attackPositions[0];
                currentAttackRotation = 0f;
                break;

            case < 0f: // down
                currentAttackPosition = attackPositions[2];
                currentAttackRotation = -180f;
                break;
        }
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }

    private void NoEnemySelectionCallback()
    {
        enemyTarget = null;
    }

    private void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent += NoEnemySelectionCallback;
        EnemyHealth.OnEnemyDeadEvent += NoEnemySelectionCallback;
    }

    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNoSelectionEvent -= NoEnemySelectionCallback;
        EnemyHealth.OnEnemyDeadEvent -= NoEnemySelectionCallback;
    }
}
