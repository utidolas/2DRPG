using UnityEngine;
using System;


[Serializable]
public class FSMTransition
{
    public FSMDecision Decision; // ex.: PlayerInRangeOfAttack *returns* True or False
    public string TrueState; // transition from: CurrentState -> AttackState (TrueState)
    public string FalseState; // transition from: CurrentState -> PatrolStat (FalseState)
}
