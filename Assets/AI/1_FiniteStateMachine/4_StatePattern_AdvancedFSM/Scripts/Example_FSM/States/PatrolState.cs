using UnityEngine;
using StatePattern_FSM;

public class PatrolState : State
{
    [Header("State Values")]
    [SerializeField] private Animator robotAnimator;
    [SerializeField] private string animator_PatrolTrigger;

    public override void Enter()
    {
        Debug.Log($"New State: <color=yellow>Patrol</color>");
        robotAnimator.SetTrigger(animator_PatrolTrigger);
    }
}