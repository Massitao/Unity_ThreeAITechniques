using UnityEngine;
using StatePattern_FSM;

public class AgressiveSSM : SubStateMachine
{
    public override void Enter()
    {
        Debug.Log($"Entered <color=red>Agressive</color> Sub State");
    }
}