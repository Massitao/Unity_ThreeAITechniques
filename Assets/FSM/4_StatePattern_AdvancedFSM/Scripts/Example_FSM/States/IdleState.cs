using UnityEngine;

public class IdleState : State
{
    [Header("State Values")]
    [SerializeField] private Animator robotAnimator;
    [SerializeField] private string animator_IdleTrigger;

    [Space(10)]

    [SerializeField] private float idleTimer;


    public override void Enter()
    {
        Debug.Log($"New State: <color=green>IDLE</color>");
        robotAnimator.SetTrigger(animator_IdleTrigger);
    }

    public override void Execute()
    {
        idleTimer += Time.deltaTime;
    }

    public override void Exit()
    {
        idleTimer = 0f;
    }


    public void ResetTimer()
    {
        idleTimer = 0f;
    }
    public float GetTimer()
    {
        return idleTimer;
    }
}