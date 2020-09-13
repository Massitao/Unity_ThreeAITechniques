using UnityEngine;

public class IdleDecision : DecisionBySpecifics
{
    [Header("Idle Considerations")]
    [SerializeField] private Consideration cantSeePlayerConsideration;
    [SerializeField] private Consideration isntPatrollingConsideration;
    [SerializeField] private Decision healDecision;


    public override float Evaluate()
    {
        decisionScore = (cantSeePlayerConsideration.Evaluate() - healDecision.Evaluate()) * isntPatrollingConsideration.Evaluate();
        return decisionScore;
    }
}