using UnityEngine;

public class IdleWaitNode : LeafNode
{
    [Header("Node Properties")]
    [SerializeField] private Robot_SimpleBT bt;

    public override void Initialize()
    {
        bt.SetIdlePauseTimer(0f);
        bt.SetRobotState(Robot_SimpleBT.RobotStates.Idle);
    }

    public override NodeStates Process()
    {
        if (debug) Debug.Log($"IDLE");

        if (bt.GetIdlePauseTimer() < bt.GetIdlePauseTime())
        {
            bt.SetIdlePauseTimer(bt.GetIdlePauseTimer() + Time.deltaTime);

            return NodeStates.Running;
        }
        else
        {
            bt.SetIdlePauseTimer(bt.GetIdlePauseTime());
            return NodeStates.Success;
        }
    }
}