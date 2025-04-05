using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float PlayerSpeed;

    private Vector2 moveDirection;

    private PlayerActions actions;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        actions = new PlayerActions(); // new instance of action class
        rb2D = GetComponent<Rigidbody2D>(); // reference of component attachted to player obj
    }

    void Start()
    {
        
    }

    void Update()
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
