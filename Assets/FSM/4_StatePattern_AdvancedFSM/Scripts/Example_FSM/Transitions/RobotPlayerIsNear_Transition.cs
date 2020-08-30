using UnityEngine;
using StatePattern_FSM;

public class RobotPlayerIsNear_Transition : StateTransitionConditions
{
    [SerializeField] private StatePatternFSM fsm;

    public override bool IsMet()
    {
        return ((fsm.GetPlayerVisibility() && !fsm.GetPlayerStatus()) && fsm.GetRobotToPlayerDistance() == StatePatternFSM.Distance.Near);
    }
}