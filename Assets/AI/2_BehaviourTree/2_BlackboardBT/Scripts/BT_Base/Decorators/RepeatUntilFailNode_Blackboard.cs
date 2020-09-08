public class RepeatUntilFailNode_Blackboard : RepeaterNode_Blackboard
{
    public override NodeStates Process(BlackboardBase bb)
    {
        NodeStates childNodeState = childNode.Process(bb);

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
                    childNode.Initialize(bb);

                    return Process(bb);
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
                        childNode.Initialize(bb);

                        return Process(bb);
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