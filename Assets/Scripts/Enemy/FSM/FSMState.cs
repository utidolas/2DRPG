using UnityEngine;
using System;

[Serializable]
public class FSMState 
{
    public string ID; // define a name (ID) for state
    public FSMAction[] Actions; // array of actions since each state can perform many actions
    public FSMTransition[] Transitions; // each state can perform many transition

    public void UpdateState(EnemyBrain enemyBrain)
    {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }

    private void ExecuteActions()
    {
        for(int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act(); // calling each action
        }
    }

    private void ExecuteTransitions(EnemyBrain enemyBrain)
    {
        if (Transitions == null || Transitions.Length <= 0) return; // if don't have transitions
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool value = Transitions[i].Decision.Decide();
            if (value)
            {
                enemyBrain.ChangeState(Transitions[i].TrueState);
            }
            else
            {
                enemyBrain.ChangeState(Transitions[i].FalseState);
            }
        }
    }
}
