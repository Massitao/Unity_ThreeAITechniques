public class InverterNode_Blackboard : DecoratorNode_Blackboard
{
    public override void Initialize(BlackboardBase bb)
    {
        childNode.Initialize(bb);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        switch (childNode.Process(bb))
        {
            case NodeStates.Success:
                return NodeStates.Failure;

            case NodeStates.Failure:
                return NodeStates.Success;
        }

        return NodeStates.Running;
    }
}