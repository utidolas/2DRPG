using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float PlayerSpeed;

    // Creating hash to animator X and Y
    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");

    private Vector2 moveDirection;

    private Animator animator;
    private PlayerActions actions;
    private Rigidbody2D rb2D;


    private void Awake()
    {
        actions = new PlayerActions(); // new instance of action class
        rb2D = GetComponent<Rigidbody2D>(); // reference of component attached to player obj
        animator = GetComponent<Animator>(); // reference of component attached to player obj
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
        rb2D.MovePosition(rb2D.position + moveDirection * (PlayerSpeed * Time.fixedDeltaTime)); // moving & fixing FPS
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized; // get dir & normalizing vector lengh
        if (moveDirection == Vector2.zero) return;
        // animation horizontal and vertical
        animator.SetFloat(moveX, moveDirection.x);
        animator.SetFloat(moveY, moveDirection.y);
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
