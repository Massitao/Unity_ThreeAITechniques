using System;
using UnityEngine;

public class Robot_SimpleBT : BehaviourTree_Simple
{
    [Header("Robot State")]
    [SerializeField] private RobotStates currentRobotState;
    public enum RobotStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Kick
    }

    [Header("Distance From Player")]
    [SerializeField] private Distance currentDistanceFromPlayer;
    public enum Distance
    {
        Far,
        Near
    }

    [Header("Idle Pause Time")]
    [SerializeField] private int idlePauseTime;
    [SerializeField] private float idlePauseTimer;

    [Header("Patrol Point Check")]
    [SerializeField] private bool reachedPatrolPoint;

    [Header("Player Visibility")]
    [SerializeField] private bool canSeePlayer;
    [SerializeField] private bool playerIsDead;


    [Header("Events")]
    public Action OnStateChange;


    private void Start()
    {
        InitializeBT();
    }


    #region Getters and Setters
    public RobotStates GetRobotState()
    {
        return currentRobotState;
    }
    public void SetRobotState(RobotStates newState)
    {
        if (currentRobotState != newState)
        {
            currentRobotState = newState;

            OnStateChange?.Invoke();
        }
    }

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
        idlePauseTimer = 0f;
        idlePauseTime = newIdleTime;
    }
    public float GetIdlePauseTimer()
    {
        return idlePauseTimer;
    }
    public void SetIdlePauseTimer(float newTimer)
    {
        idlePauseTimer = newTimer;
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