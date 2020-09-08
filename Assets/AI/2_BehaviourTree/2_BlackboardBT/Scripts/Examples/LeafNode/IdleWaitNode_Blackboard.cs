using UnityEngine;

public class IdleWaitNode_Blackboard : LeafNode_Blackboard
{
    public override void Initialize(BlackboardBase bb)
    {
        RobotBlackboard robotBB = bb as RobotBlackboard;

        robotBB.SetIdlePauseTimer(0f);
        robotBB.SetRobotState(RobotBlackboard.RobotStates.Idle);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        if (debug) Debug.Log($"IDLE");

        RobotBlackboard robotBB = bb as RobotBlackboard;

        if (robotBB.GetIdlePauseTimer() < robotBB.GetIdlePauseTime())
        {
            robotBB.SetIdlePauseTimer(robotBB.GetIdlePauseTimer() + Time.deltaTime);

            return NodeStates.Running;
        }
        else
        {
            robotBB.SetIdlePauseTimer(robotBB.GetIdlePauseTime());
            return NodeStates.Success;
        }
    }
}