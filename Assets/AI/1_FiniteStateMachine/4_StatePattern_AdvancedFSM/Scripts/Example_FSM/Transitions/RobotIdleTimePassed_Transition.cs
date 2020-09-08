using UnityEngine;
using StatePattern_FSM;

public class RobotIdleTimePassed_Transition : StateTransitionConditions
{
    [SerializeField] private StatePatternFSM fsm;
    [SerializeField] private IdleState idleState;

    public override bool IsMet()
    {
        return (idleState.GetTimer() >= fsm.GetIdlePauseTime());
    }
}