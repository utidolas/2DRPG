using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private string initState; // initial state
    [SerializeField] private FSMState[] states;
    public FSMState CurrentState { get; set; }

    private void Start()
    {
        ChangeState(initState);
    }

    private void Update()
    {
        // '?' is the same as having : if ( CurrentState == null ) return;
        CurrentState?.UpdateState(this);
    }

    public void ChangeState(string newStateID)
    {
        // check if newstateID existis in states arra & if find state with "newStateID", store it in "newState"
        FSMState newState = GetState(newStateID);
        if (newState == null) return;
        CurrentState = newState;
    }

    private FSMState GetState(string newStateID)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].ID == newStateID)
            {
                return states[i];
            }
        }

        return null;
    }
}
