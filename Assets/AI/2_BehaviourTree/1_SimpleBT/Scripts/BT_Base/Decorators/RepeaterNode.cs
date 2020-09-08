public class RepeaterNode : DecoratorNode
{
    public bool infinite;

    public int timesToRepeat;
    protected int repeatedTimes;

    public override void Initialize()
    {
        repeatedTimes = 0;
        childNode.Initialize();
    }

    public override NodeStates Process()
    {
        if (infinite)
        {
            return NodeStates.Running;
        }

        if (childNode.Process() != NodeStates.Running)
        {
            if (repeatedTimes >= timesToRepeat)
            {
                return NodeStates.Success;
            }
            else
            {
                repeatedTimes++;
                childNode.Initialize();

                return Process();
            }
        }
        else
        {
            return NodeStates.Running;
        }
    }
}