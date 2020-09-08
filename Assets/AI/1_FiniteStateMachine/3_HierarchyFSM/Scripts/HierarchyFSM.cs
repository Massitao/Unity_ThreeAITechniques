using System;
using UnityEngine;

namespace Hierarchy_FSM
{
    public class HierarchyFSM : MonoBehaviour
    {
        [Header("Robot State")]
        [SerializeField] private RobotGroupStates currentRobotGroupState;
        public enum RobotGroupStates
        {
            Relaxed,
            Agressive
        }

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


        [Header("Events")]
        public Action OnStateChange;


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

                    if (idlePauseTimer >= idlePauseTime)
                    {
                        SetRobotState(RobotStates.Patrol);
                        return;
                    }

                    break;

                case RobotStates.Patrol:
                    if (reachedPatrolPoint)
                    {
                        SetRobotState(RobotStates.Idle);
                        return;
                    }

                    break;
            }

            CheckOnPlayer();
        }

        private void SetGroupRobotState(RobotGroupStates newGroupRobotState, bool doSwitch)
        {
            if (newGroupRobotState == currentRobotGroupState)
            {
                return;
            }

            Debug.Log($"Changing Robot State from <color={GetGroupStateColor(currentRobotGroupState)}>{currentRobotGroupState.ToString()}</color> to <color={GetGroupStateColor(newGroupRobotState)}>{newGroupRobotState.ToString()}</color>");

            currentRobotGroupState = newGroupRobotState;

            if (doSwitch)
            {
                switch (newGroupRobotState)
                {
                    case RobotGroupStates.Relaxed:
                        SetRobotState(RobotStates.Idle);

                        break;

                    case RobotGroupStates.Agressive:
                        SetRobotState(RobotStates.Chase);

                        break;
                }
            }
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

            switch (newRobotState)
            {
                case RobotStates.Idle:
                    SetGroupRobotState(RobotGroupStates.Relaxed, false);

                    break;

                case RobotStates.Patrol:
                    SetGroupRobotState(RobotGroupStates.Relaxed, false);

                    break;

                case RobotStates.Chase:
                    SetGroupRobotState(RobotGroupStates.Agressive, false);

                    break;

                case RobotStates.Attack:
                    SetGroupRobotState(RobotGroupStates.Agressive, false);

                    break;
            }

            Debug.Log($"Changing Robot State from <color={GetStateColor(currentRobotState)}>{currentRobotState.ToString()}</color> to <color={GetStateColor(newRobotState)}>{newRobotState.ToString()}</color>");

            currentRobotState = newRobotState;
            OnStateChange?.Invoke();
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
                    SetGroupRobotState(RobotGroupStates.Relaxed, true);
                }
            }
        }


        #region Getters and Setters
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
        private string GetGroupStateColor(RobotGroupStates state)
        {
            switch (state)
            {
                case RobotGroupStates.Relaxed:
                    return "green";

                case RobotGroupStates.Agressive:
                    return "red";
            }
            return "white";
        }

        public RobotGroupStates GetRobotGroupStates()
        {
            return currentRobotGroupState;
        }
        public RobotStates GetRobotState()
        {
            return currentRobotState;
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
}