using UnityEngine;

public class ActionWander : FSMAction
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private float wanderTime; // move to a new loc every 'wanderTime'
    [SerializeField] private Vector2 moveRange; // movement range 

    private Vector3 movePosition; // next movePosition
    private float timer; // to control 'wanderTime'

    private void Start()
    {
        GetNewDestination();
    }

    public override void Act()
    {
        timer -= Time.deltaTime;
        Vector3 moveDirection = (movePosition - transform.position).normalized; // storing the direction
        Vector3 movement = moveDirection * (speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePosition) >= .5f) //
        {
            transform.Translate(movement);
        }

        if(timer <= 0f)
        {
            GetNewDestination();
            timer = wanderTime;
        }
    }

    private void GetNewDestination()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x); // gettin a random x pos inside the moveRange
        float randomY = Random.Range(-moveRange.y, moveRange.y); // gettin a random y pos inside the moveRange
        movePosition = transform.position + new Vector3(randomX, randomY);
    }

    private void OnDrawGizmosSelected()
    {
        if(moveRange != Vector2.zero) 
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, moveRange * 2f);
            Gizmos.DrawLine(transform.position, movePosition);
        }
    }
}
