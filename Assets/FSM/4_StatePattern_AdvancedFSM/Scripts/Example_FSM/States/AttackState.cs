using UnityEngine;

public class AttackState : State
{
    [Header("State Values")]
    [SerializeField] private Animator robotAnimator;
    [SerializeField] private string animator_AttackTrigger;

    public override void Enter()
    {
        Debug.Log($"New State: <color=red>Attack</color>");
        robotAnimator.SetTrigger(animator_AttackTrigger);
    }
}