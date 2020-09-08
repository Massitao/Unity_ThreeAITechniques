using UnityEngine;

public class PatrolNode : LeafNode
{
    [Header("Node Properties")]
    [SerializeField] private Robot_SimpleBT bt;


    public override void Initialize()
    {
        bt.SetRobotState(Robot_SimpleBT.RobotStates.Patrol);
    }

    public override NodeStates Process()
    {
        if (debug) Debug.Log($"PATROL");

        if (!bt.GetPatrolReachedPoint())
        {
            return NodeStates.Running;
        }
        else
        {
            bt.SetPatrolReachedPoint(false);
            return NodeStates.Success;
        }
    }
}