public class RepeatUntilFailNode : RepeaterNode
{
    public override NodeStates Process()
    {
        NodeStates childNodeState = childNode.Process();

        if (childNodeState != NodeStates.Running)
        {
            if (childNodeState == NodeStates.Failure)
            {
                return NodeStates.Success;
            }
            else
            {
                if (infinite)
                {
                    childNode.Initialize();

                    return Process();
                }
                else
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
            }
        }
        else
        {
            return NodeStates.Running;
        }
    }
}