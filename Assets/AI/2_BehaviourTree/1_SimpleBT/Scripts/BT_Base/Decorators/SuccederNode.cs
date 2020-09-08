public class SuccederNode : DecoratorNode
{
    public override void Initialize()
    {
        childNode.Initialize();
    }

    public override NodeStates Process()
    {
        if (childNode.Process() != NodeStates.Running)
        {
            return NodeStates.Success;
        }
        else
        {
            return NodeStates.Running;
        }
    }
}