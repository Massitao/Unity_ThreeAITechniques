using UnityEngine;

public class PatrolNode_Blackboard : LeafNode_Blackboard
{
    public override void Initialize(BlackboardBase bb)
    {
        RobotBlackboard robotBB = bb as RobotBlackboard;

        robotBB.SetRobotState(RobotBlackboard.RobotStates.Patrol);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        if (debug) Debug.Log($"PATROL");

        RobotBlackboard robotBB = bb as RobotBlackboard;

        if (!robotBB.GetPatrolReachedPoint())
        {
            return NodeStates.Running;
        }
        else
        {
            robotBB.SetPatrolReachedPoint(false);
            return NodeStates.Success;
        }
    }
}