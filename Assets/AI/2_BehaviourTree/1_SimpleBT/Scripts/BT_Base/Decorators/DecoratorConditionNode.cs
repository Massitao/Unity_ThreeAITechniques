public abstract class DecoratorConditionNode : DecoratorNode
{
    public bool invertCondition;

    public override void Initialize()
    {
        bool conditionMet = (!invertCondition) ? IsMet() : !IsMet();

        if (conditionMet) childNode.Initialize();
    }

    public override NodeStates Process()
    {
        bool conditionMet = (!invertCondition) ? IsMet() : !IsMet();

        if (conditionMet)
        {
            return childNode.Process();
        }
        else
        {
            return NodeStates.Failure;
        }
    }

    public abstract bool IsMet();
}