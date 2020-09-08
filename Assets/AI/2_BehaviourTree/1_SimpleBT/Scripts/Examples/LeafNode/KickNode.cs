using UnityEngine;

public class KickNode : LeafNode
{
    [Header("Node Properties")]
    [SerializeField] private Robot_SimpleBT bt;

    [SerializeField] private float attackTime = 1f;
    private float timer;

    public override void Initialize()
    {
        timer = 0f;
        bt.SetRobotState(Robot_SimpleBT.RobotStates.Kick);
    }

    public override NodeStates Process()
    {
        if (debug) Debug.Log($"KICK");

        if (timer < attackTime)
        {
            timer += Time.deltaTime;
            return NodeStates.Running;
        }
        else
        {
            return NodeStates.Success;
        }
    }
}