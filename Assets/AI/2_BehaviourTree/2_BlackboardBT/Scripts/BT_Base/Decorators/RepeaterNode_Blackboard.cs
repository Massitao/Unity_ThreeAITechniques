public class RepeaterNode_Blackboard : DecoratorNode_Blackboard
{
    public bool infinite;

    public int timesToRepeat;
    protected int repeatedTimes;

    public override void Initialize(BlackboardBase bb)
    {
        repeatedTimes = 0;
        childNode.Initialize(bb);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        if (infinite)
        {
            return NodeStates.Running;
        }

        if (childNode.Process(bb) != NodeStates.Running)
        {
            if (repeatedTimes >= timesToRepeat)
            {
                return NodeStates.Success;
            }
            else
            {
                repeatedTimes++;
                childNode.Initialize(bb);

                return Process(bb);
            }
        }
        else
        {
            return NodeStates.Running;
        }
    }
}