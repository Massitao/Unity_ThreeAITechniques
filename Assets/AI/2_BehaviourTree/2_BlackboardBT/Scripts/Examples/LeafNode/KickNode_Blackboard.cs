using UnityEngine;

public class KickNode_Blackboard : LeafNode_Blackboard
{
    [Header("Node Properties")]
    [SerializeField] private float attackTime = 1f;
    private float timer;

    public override void Initialize(BlackboardBase bb)
    {
        timer = 0f;

        RobotBlackboard robotBB = bb as RobotBlackboard;

        robotBB.SetRobotState(RobotBlackboard.RobotStates.Kick);
    }

    public override NodeStates Process(BlackboardBase bb)
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