using System;
using UnityEngine;

public class RobotUtilityBlackboard : BlackboardBase
{
    [Header("Robot Status")]
    public string currentDecisionName;

    [SerializeField] private float robotHealth;
    [SerializeField] private float robotMaxHealth;

    public Action OnRobotHealthChange;
    public Action OnRobotMaxHealthChange;


    [Header("Idle Pause Time")]
    [SerializeField] private int idlePauseTime;
    [SerializeField] private float idlePauseTimer;

    public Action OnRobotIdlePauseTimeChange;
    public Action OnRobotIdlePauseTimerChange;


    [Header("Patrol Point Check")]
    [SerializeField] private bool reachedPatrolPoint;
    [SerializeField] private bool patrolling;

    public Action OnReachedPatrolPointChange;
    public Action OnPatrollingChange;


    [Header("Distance From Player")]
    [SerializeField] private float currentPlayerToRobotDistance;
    [SerializeField] private float farDistance;
    [SerializeField] private float closeDistance;

    public Action OnCurrentDistanceChange;
    public Action OnFarDistanceChange;
    public Action OnCloseDistanceChange;


    [Header("Attack Time")]
    [SerializeField] private float attackTime;


    [Header("Player Visibility")]
    [SerializeField] private bool canSeePlayer;

    public Action OnPlayerVisibilityChange;


    [Header("Player Status")]
    [SerializeField] private bool playerIsDead;

    public Action OnPlayerStatusChange;

    [Header("Any Activity Action")]
    public Action OnAnyChange;


    #region Getters and Setters
    public float GetRobotHealth()
    {
        return robotHealth;
    }
    public void SetRobotHealth(float newHealth)
    {
        robotHealth = Mathf.Clamp(newHealth, 1f, robotMaxHealth);
        OnRobotHealthChange?.Invoke();
        OnAnyChange?.Invoke();
    }
    public float GetRobotMaxHealth()
    {
        return robotMaxHealth;
    }
    public void SetRobotMaxHealth(float newMaxHealth)
    {
        robotMaxHealth = newMaxHealth;
        OnRobotMaxHealthChange?.Invoke();
        OnAnyChange?.Invoke();
    }

    public int GetIdlePauseTime()
    {
        return idlePauseTime;
    }
    public void SetIdlePauseTime(int newIdleTime)
    {
        idlePauseTimer = 0f;
        idlePauseTime = newIdleTime;

        OnRobotIdlePauseTimeChange?.Invoke();
        OnAnyChange?.Invoke();
    }
    public float GetIdlePauseTimer()
    {
        return idlePauseTimer;
    }
    public void SetIdlePauseTimer(float newIdleTimer)
    {
        idlePauseTimer = newIdleTimer;

        OnRobotIdlePauseTimerChange?.Invoke();
    }

    public bool GetPatrolReachedPoint()
    {
        return reachedPatrolPoint;
    }
    public void SetPatrolReachedPoint(bool reached)
    {
        reachedPatrolPoint = reached;
        OnReachedPatrolPointChange?.Invoke();
        OnAnyChange?.Invoke();
    }
    public bool GetPatrolling()
    {
        return patrolling;
    }
    public void SetPatrolling(bool onPatrol)
    {
        patrolling = onPatrol;
        OnPatrollingChange?.Invoke();
        OnAnyChange?.Invoke();
    }

    public float GetRobotToPlayerDistance()
    {
        return currentPlayerToRobotDistance;
    }
    public void SetRobotToPlayerDistance(float newDistance)
    {
        currentPlayerToRobotDistance = newDistance;
        OnCurrentDistanceChange?.Invoke();
        OnAnyChange?.Invoke();
    }
    public float GetFarDistance()
    {
        return farDistance;
    }
    public void SetFarDistance(float newFarDistance)
    {
        farDistance = newFarDistance;
        OnFarDistanceChange?.Invoke();
        OnAnyChange?.Invoke();
    }
    public float GetCloseDistance()
    {
        return closeDistance;
    }
    public void SetCloseDistance(float newCloseDistance)
    {
        closeDistance = newCloseDistance;
        OnCloseDistanceChange?.Invoke();
        OnAnyChange?.Invoke();
    }

    public float GetAttackTime()
    {
        return attackTime;
    }

    public bool GetPlayerVisibility()
    {
        return canSeePlayer;
    }
    public void SetPlayerVisibility(bool visible)
    {
        canSeePlayer = visible;
        OnPlayerVisibilityChange?.Invoke();

        if (canSeePlayer) SetPlayerStatus(false);
        else OnAnyChange?.Invoke();
    }

    public bool GetPlayerStatus()
    {
        return playerIsDead;
    }
    public void SetPlayerStatus(bool dead)
    {
        playerIsDead = dead;
        OnPlayerStatusChange?.Invoke();

        if (playerIsDead) SetPlayerVisibility(false);
        else OnAnyChange?.Invoke();
    }
    #endregion
}