using UnityEngine;

public class FleeDecision : DecisionBySpecifics
{
    [Header("Flee Considerations")]
    [SerializeField] private Consideration canSeePlayerConsideration;
    [SerializeField] private Consideration playerIsNearConsideration;
    [SerializeField] private Decision healDecision;

    public override float Evaluate()
    {
        decisionScore = (playerIsNearConsideration.Evaluate() * 2) * canSeePlayerConsideration.Evaluate() * healDecision.Evaluate();
        return decisionScore;
    }
}