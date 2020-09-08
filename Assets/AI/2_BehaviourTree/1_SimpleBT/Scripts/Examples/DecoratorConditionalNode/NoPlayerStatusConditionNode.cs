using UnityEngine;

public class NoPlayerStatusConditionNode : DecoratorConditionNode
{
    [Header("Node Properties")]
    [SerializeField] private Robot_SimpleBT bt;

    public override bool IsMet()
    {
        return (!bt.GetPlayerVisibility() || bt.GetPlayerStatus());
    }
}