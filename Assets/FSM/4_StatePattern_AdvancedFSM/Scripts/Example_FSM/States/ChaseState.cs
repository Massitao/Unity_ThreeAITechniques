using UnityEngine;

public class ChaseState : State
{
    [Header("State Values")]
    [SerializeField] private Animator robotAnimator;
    [SerializeField] private string animator_ChaseTrigger;

    public override void Enter()
    {
        Debug.Log($"New State: <color=orange>CHASE</color>");
        robotAnimator.SetTrigger(animator_ChaseTrigger);
    }
}