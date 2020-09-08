using UnityEngine;

public class IsPlayerDeadNode : LeafConditionNode
{
    [Header("Node Properties")]
    [SerializeField] private Robot_SimpleBT bt;


    public override void Initialize()
    {
        // Nothing to initialize
    }

    public override bool IsMet()
    {
        return bt.GetPlayerStatus();
    }
}