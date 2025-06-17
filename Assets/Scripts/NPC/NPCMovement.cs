using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed;

    private readonly int moveX = Animator.StringToHash("MoveX");
    private readonly int moveY = Animator.StringToHash("MoveY");

    private Waypoint waypoint;
    private Animator animator;
    private Vector3 previousPos;
    private int currentPointIndex; // to use points of waypoint
    

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 nextPos = waypoint.GetPosition(currentPointIndex);
        UpdateMoveValues(nextPos);
        // move NPC
        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, nextPos) <= 0.2)
        {
            previousPos = nextPos;
            // add 1 to currentPointIndex and set it to zero in case it go beyond 'waypoint.Points.Length'
            currentPointIndex = (currentPointIndex + 1) % waypoint.Points.Length;
        }
    }

    private void UpdateMoveValues(Vector3 nextPos)
    {
        Vector2 dir = Vector2.zero;

        // if nextPos is to the right, move X value positive. Otherwise, do the opposite.
        if (previousPos.x < nextPos.x) dir = new Vector2(1f, 0f);
        if (previousPos.x > nextPos.x) dir = new Vector2(-1f, 0f);

        // if nextPos is above, move Y value positive. Otherwise, do the opposite.
        if (previousPos.y < nextPos.y) dir = new Vector2(0f, 1f);
        if (previousPos.y > nextPos.y) dir = new Vector2(0f, -1f);

        // update animation
        animator.SetFloat(moveX, dir.x);
        animator.SetFloat(moveY, dir.y);

    }
}
