using UnityEngine;
using StatePattern_FSM;

public class RobotReachedPatrolPoint_Transition : StateTransitionConditions
{
    [SerializeField] private StatePatternFSM fsm;

    public override bool IsMet()
    {
        if (fsm.GetPatrolReachedPoint())
        {
            fsm.SetPatrolReachedPoint(false);
            return true;
        }
        else
        {
            return false;
        }
    }
}