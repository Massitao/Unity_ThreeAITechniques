public abstract class DecoratorConditionNode_Blackboard : DecoratorNode_Blackboard
{
    public bool invertCondition;

    public override void Initialize(BlackboardBase bb)
    {
        bool conditionMet = (!invertCondition) ? IsMet(bb) : !IsMet(bb);

        if (conditionMet) childNode.Initialize(bb);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        bool conditionMet = (!invertCondition) ? IsMet(bb) : !IsMet(bb);

        if (conditionMet)
        {
            return childNode.Process(bb);
        }
        else
        {
            return NodeStates.Failure;
        }
    }

    public abstract bool IsMet(BlackboardBase bb);
}