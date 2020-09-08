public class CanSeePlayerNode_Blackboard : LeafConditionNode_Blackboard
{
    public override void Initialize(BlackboardBase bb)
    {
        // Nothing to initialize
    }

    public override bool IsMet(BlackboardBase bb)
    {
        RobotBlackboard robotBB = bb as RobotBlackboard;

        return robotBB.GetPlayerVisibility();
    }
}