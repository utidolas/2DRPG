using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float PlayerSpeed;

    public Vector2 MoveDirection => moveDirection; //prop

    private PlayerAnimations playerAnimations; // reference of script "PlayerAnimations.cs"
    private Player player; // reference of script "Player.cs"

    private Vector2 moveDirection;

    private PlayerActions actions;
    private Rigidbody2D rb2D;


    private void Awake()
    {
        actions = new PlayerActions(); // new instance of action class
        rb2D = GetComponent<Rigidbody2D>(); // reference of component attached to player obj
        playerAnimations = GetComponent<PlayerAnimations>(); // reference of script "PlayerAnimations.cs"
        player = GetComponent<Player>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        ReadMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (player.Stats.Health <= 0f) return;
        rb2D.MovePosition(rb2D.position + moveDirection * (PlayerSpeed * Time.fixedDeltaTime)); // moving & fixing FPS
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized; // get dir & normalizing vector lengh
        if (moveDirection == Vector2.zero) 
        {
            playerAnimations.SetMovingAnimation(false);
            return;
        }

        // animation horizontal and vertical
        playerAnimations.SetMovingAnimation(true);
        playerAnimations.SetMoveAnimation(moveDirection);
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
