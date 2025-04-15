using Unity.VisualScripting;
using UnityEngine;

public class ActionPatrol : FSMAction
{
    [Header("Config")]
    [SerializeField] float speed;

    private Waypoint waypoint;
    private int waypointIndex;
    private Vector3 nextPosition;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    public override void Act()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetCurrentPosition(), speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, GetCurrentPosition()) <= .1f)
        {
            UpdateNextPosition();
        }
    }

    private void UpdateNextPosition()
    {
        waypointIndex++;
        if(waypointIndex > waypoint.Points.Length - 1) 
        { 
            waypointIndex = 0;
        }  
    }

    private Vector3 GetCurrentPosition()
    {
        return waypoint.GetPosition(waypointIndex);
    }
}
