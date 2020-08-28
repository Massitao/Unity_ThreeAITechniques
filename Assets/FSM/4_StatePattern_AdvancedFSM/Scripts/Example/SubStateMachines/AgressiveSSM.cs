using UnityEngine;

public class AgressiveSSM : SubStateMachine
{
    public override void Enter()
    {
        Debug.Log($"Entered <color=red>Agressive</color> Sub State");
    }
    public override void Exit()
    {
        Debug.Log($"Exited <color=red>Agressive</color> Sub State");
    }

}