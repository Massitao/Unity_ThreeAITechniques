using UnityEngine;

public class RobotLostPlayer_Transition : StateTransitionConditions
{
    [SerializeField] private StatePatternFSM fsm;

    public override bool IsMet()
    {
        return (!fsm.GetPlayerVisibility() || fsm.GetPlayerStatus());
    }
}