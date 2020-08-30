using System;
using UnityEngine;
using StatePattern_FSM;

public class StatePatternFSM : StateMachine
{
    [Header("Sub State Machines")]
    [SerializeField] private RelaxedSSM relaxedSubState;
    [SerializeField] private AgressiveSSM agressiveSubState;

    [Header("States")]
    [SerializeField] private IdleState idleState;
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private AttackState attackState;

    [Header("Distance From Player")]
    [SerializeField] private Distance currentDistanceFromPlayer;
    public enum Distance
    {
        Far,
        Near
    }

    [Header("Idle Pause Time")]
    [SerializeField] private int idlePauseTime;

    [Header("Patrol Point Check")]
    [SerializeField] private bool reachedPatrolPoint;

    [Header("Player Visibility")]
    [SerializeField] private bool canSeePlayer;
    [SerializeField] private bool playerIsDead;

    [Header("Events")]
    public Action OnStateChange;


    private void Awake()
    {
        InitializeFSM(false);
    }

    protected override void SetState(State newState)
    {
        base.SetState(newState);

        OnStateChange?.Invoke();
    }
    protected override void SetSubStateMachine(SubStateMachine ssm, bool entryToStateMachine)
    {
        base.SetSubStateMachine(ssm, entryToStateMachine);

        OnStateChange?.Invoke();
    }

    #region Getters and Setters
    public Distance GetRobotToPlayerDistance()
    {
        return currentDistanceFromPlayer;
    }
    public void SetRobotToPlayerDistance(Distance newDistance)
    {
        currentDistanceFromPlayer = newDistance;
    }

    public int GetIdlePauseTime()
    {
        return idlePauseTime;
    }
    public void SetIdlePauseTime(int newIdleTime)
    {
        idleState.ResetTimer();
        idlePauseTime = newIdleTime;
    }
    public float GetIdlePauseTimer()
    {
        return idleState.GetTimer();
    }

    public bool GetPatrolReachedPoint()
    {
        return reachedPatrolPoint;
    }
    public void SetPatrolReachedPoint(bool reached)
    {
        reachedPatrolPoint = reached;
    }

    public bool GetPlayerVisibility()
    {
        return canSeePlayer;
    }
    public void SetPlayerVisibility(bool visible)
    {
        canSeePlayer = visible;
    }

    public bool GetPlayerStatus()
    {
        return playerIsDead;
    }
    public void SetPlayerStatus(bool dead)
    {
        playerIsDead = dead;
    }
    #endregion
}