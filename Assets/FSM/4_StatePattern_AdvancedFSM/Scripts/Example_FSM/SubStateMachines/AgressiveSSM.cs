using UnityEngine;

public class AgressiveSSM : SubStateMachine
{
    public override void Enter()
    {
        Debug.Log($"Entered <color=red>Agressive</color> Sub State");
    }
}