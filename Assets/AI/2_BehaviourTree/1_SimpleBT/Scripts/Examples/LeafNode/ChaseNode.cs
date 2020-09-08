using UnityEngine;

public class ChaseNode : LeafNode
{
    [Header("Node Properties")]
    [SerializeField] private Robot_SimpleBT bt;


    public override void Initialize()
    {
        bt.SetRobotState(Robot_SimpleBT.RobotStates.Chase);
    }

    public override NodeStates Process()
    {
        if (debug) Debug.Log($"CHASE");

        if (bt.GetRobotToPlayerDistance() == Robot_SimpleBT.Distance.Far)
        {
            return NodeStates.Running;
        }
        else
        {
            return NodeStates.Failure;
        }
    }
}