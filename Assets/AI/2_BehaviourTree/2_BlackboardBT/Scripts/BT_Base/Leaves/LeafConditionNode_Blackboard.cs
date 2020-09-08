public abstract class LeafConditionNode_Blackboard : LeafNode_Blackboard
{
    public override NodeStates Process(BlackboardBase bb)
    {
        if (IsMet(bb))
        {
            return NodeStates.Success;
        }
        else
        {
            return NodeStates.Failure;
        }
    }

    public abstract bool IsMet(BlackboardBase bb);
}