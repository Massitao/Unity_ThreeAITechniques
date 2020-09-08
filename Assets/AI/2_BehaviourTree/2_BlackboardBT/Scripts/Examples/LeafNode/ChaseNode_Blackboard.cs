using UnityEngine;

public class ChaseNode_Blackboard : LeafNode_Blackboard
{
    public override void Initialize(BlackboardBase bb)
    {
        RobotBlackboard robotBB = bb as RobotBlackboard;

        robotBB.SetRobotState(RobotBlackboard.RobotStates.Chase);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        if (debug) Debug.Log($"CHASE");

        RobotBlackboard robotBB = bb as RobotBlackboard;

        if (robotBB.GetRobotToPlayerDistance() == RobotBlackboard.Distance.Far)
        {
            return NodeStates.Running;
        }
        else
        {
            return NodeStates.Failure;
        }
    }
}