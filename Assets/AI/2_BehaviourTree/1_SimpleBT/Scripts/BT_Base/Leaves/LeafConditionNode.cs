public abstract class LeafConditionNode : LeafNode
{
    public override NodeStates Process()
    {
        if (IsMet())
        {
            return NodeStates.Success;
        }
        else
        {
            return NodeStates.Failure;
        }
    }

    public abstract bool IsMet();
}