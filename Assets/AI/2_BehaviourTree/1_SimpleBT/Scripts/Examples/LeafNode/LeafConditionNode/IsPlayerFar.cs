using UnityEngine;

public class IsPlayerFar : LeafConditionNode
{
    [Header("Node Properties")]
    [SerializeField] private Robot_SimpleBT bt;


    public override void Initialize()
    {
        // Nothing to initialize
    }

    public override bool IsMet()
    {
        return bt.GetRobotToPlayerDistance() == Robot_SimpleBT.Distance.Far;
    }
}