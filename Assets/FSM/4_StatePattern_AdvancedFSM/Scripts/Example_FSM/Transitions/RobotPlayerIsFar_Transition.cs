using UnityEngine;

public class RobotPlayerIsFar_Transition : StateTransitionConditions
{
    [SerializeField] private StatePatternFSM fsm;

    public override bool IsMet()
    {
        return ((fsm.GetPlayerVisibility() && !fsm.GetPlayerStatus()) && fsm.GetRobotToPlayerDistance() == StatePatternFSM.Distance.Far);
    }
}