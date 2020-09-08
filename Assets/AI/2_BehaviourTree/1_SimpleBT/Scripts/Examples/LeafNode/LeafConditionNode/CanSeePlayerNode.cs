using UnityEngine;

public class CanSeePlayerNode : LeafConditionNode
{
    [Header("Node Properties")]
    [SerializeField] private Robot_SimpleBT bt;


    public override void Initialize()
    {
        // Nothing to initialize
    }

    public override bool IsMet()
    {
        return bt.GetPlayerVisibility();
    }
}