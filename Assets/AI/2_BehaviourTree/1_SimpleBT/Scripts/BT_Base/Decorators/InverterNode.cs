public class InverterNode : DecoratorNode
{
    public override void Initialize()
    {
        childNode.Initialize();
    }

    public override NodeStates Process()
    {
        switch (childNode.Process())
        {
            case NodeStates.Success:
                return NodeStates.Failure;

            case NodeStates.Failure:
                return NodeStates.Success;
        }

        return NodeStates.Running;
    }
}