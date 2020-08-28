using UnityEngine;

public class RelaxedSSM : SubStateMachine
{
    public override void Enter()
    {
        Debug.Log($"Entered <color=green>Relaxed</color> Sub State");
    }
    public override void Exit()
    {
        Debug.Log($"Exited <color=green>Relaxed</color> Sub State");
    }
}