using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimations : MonoBehaviour
{
    // Creating hash to player animations
    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");
    private readonly int moving = Animator.StringToHash("Moving");
    private readonly int dead = Animator.StringToHash("IsDead");
    private readonly int revive = Animator.StringToHash("Revive");
    private readonly int attacking = Animator.StringToHash("Attacking");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // reference of component attached to player obj
    }

    public void SetDeadAnimation()
    {
        animator.SetTrigger(dead);
    }

    public void SetMovingAnimation(bool value)
    {
        animator.SetBool(moving, value);
    }

    public void SetMoveAnimation(Vector2 dir)
    {
        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);
    }

    public void SetAttackAnimation(bool value)
    {
        animator.SetBool(attacking, value);
    }

    public void ResetPlayerAnimation()
    {
        SetMoveAnimation(Vector2.down); // Face down
        animator.SetTrigger(revive); // Dead anim to idle
    }
}
