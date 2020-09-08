public class SuccederNode_Blackboard : DecoratorNode_Blackboard
{
    public override void Initialize(BlackboardBase bb)
    {
        childNode.Initialize(bb);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        if (childNode.Process(bb) != NodeStates.Running)
        {
            return NodeStates.Success;
        }
        else
        {
            return NodeStates.Running;
        }
    }
}