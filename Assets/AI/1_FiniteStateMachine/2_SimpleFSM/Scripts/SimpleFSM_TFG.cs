using UnityEngine;

public class SimpleFSM_TFG : MonoBehaviour
{
    [Header("Robot State")]
    [SerializeField] private RobotStates currentRobotState;
    public enum RobotStates
    {
        Idle,
        Patrol,
        Chase,
        Attack
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


    // Update is called once per frame
    void Update()
    {
        StateUpdate();
    }

    private void StateUpdate()
    {
        switch (currentRobotState)
        {
            case RobotStates.Idle:
                idlePauseTimer += Time.deltaTime;

                if (idlePauseTimer < idlePauseTime)
                {
                    Debug.Log($"<color=green>IDLE</color>");
                }
                else
                {
                    SetRobotState(RobotStates.Patrol);
                    return;
                }

                break;

            case RobotStates.Patrol:
                if (!reachedPatrolPoint)
                {
                    Debug.Log($"<color=yellow>PATROL</color>");
                }
                else
                {
                    SetRobotState(RobotStates.Idle);
                    return;
                }

                break;

            case RobotStates.Chase:
                Debug.Log($"<color=orange>CHASE</color>");

                break;

            case RobotStates.Attack:
                Debug.Log($"<color=red>ATTACK</color>");

                break;
        }

        CheckOnPlayer();
    }
    private void SetRobotState(RobotStates newRobotState)
    {
        if (newRobotState == currentRobotState)
        {
            return;
        }

        switch (currentRobotState)
        {
            case RobotStates.Idle:
                idlePauseTimer = 0f;

                break;

            case RobotStates.Patrol:
                reachedPatrolPoint = false;

                break;
        }

        Debug.Log($"Changing Robot State from <color={GetStateColor(currentRobotState)}>{currentRobotState.ToString()}</color> to <color={GetStateColor(newRobotState)}>{newRobotState.ToString()}</color>");

        currentRobotState = newRobotState;
    }

    private void CheckOnPlayer()
    {
        if (canSeePlayer && !playerIsDead)
        {
            switch (currentDistanceFromPlayer)
            {
                case Distance.Far:
                    SetRobotState(RobotStates.Chase);

                    break;

                case Distance.Near:
                    SetRobotState(RobotStates.Attack);

                    break;
            }
        }
        else
        {
            if (currentRobotState != RobotStates.Idle && currentRobotState != RobotStates.Patrol)
            {
                SetRobotState(RobotStates.Idle);
            }
        }
    }

    private string GetStateColor(RobotStates state)
    {
        switch (state)
        {
            case RobotStates.Idle:
                return "green";

            case RobotStates.Patrol:
                return "yellow";

            case RobotStates.Chase:
                return "orange";

            case RobotStates.Attack:
                return "red";
        }
        return "white";
    }
}