public class NoPlayerStatusConditionNode_Blackboard : DecoratorConditionNode_Blackboard
{
    public override bool IsMet(BlackboardBase bb)
    {
        RobotBlackboard robotBB = bb as RobotBlackboard;

        return (!robotBB.GetPlayerVisibility() || robotBB.GetPlayerStatus());
    }
}