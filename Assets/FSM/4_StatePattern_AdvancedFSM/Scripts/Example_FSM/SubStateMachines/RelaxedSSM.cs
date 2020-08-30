using UnityEngine;
using StatePattern_FSM;

public class RelaxedSSM : SubStateMachine
{
    public override void Enter()
    {
        Debug.Log($"Entered <color=green>Relaxed</color> Sub State");
    }
}