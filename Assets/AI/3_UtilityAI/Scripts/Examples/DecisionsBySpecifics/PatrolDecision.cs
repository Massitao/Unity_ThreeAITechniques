using UnityEngine;

public class PatrolDecision : DecisionBySpecifics
{
    [Header("Patrol Considerations")]
    [SerializeField] private Consideration cantSeePlayerConsideration;
    [SerializeField] private Consideration isPatrollingConsideration;
    [SerializeField] private Decision healDecision;


    public override float Evaluate()
    {
        decisionScore = (cantSeePlayerConsideration.Evaluate() - healDecision.Evaluate()) * isPatrollingConsideration.Evaluate();
        return decisionScore;
    }
}