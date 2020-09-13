using UnityEngine;

public class AttackDecision : DecisionBySpecifics
{
    [Header("Attack Considerations")]
    [SerializeField] private Consideration canSeePlayerConsideration;
    [SerializeField] private Consideration playerIsNearConsideration;
    [SerializeField] private Decision healDecision;


    public override float Evaluate()
    {
        decisionScore = (playerIsNearConsideration.Evaluate() - healDecision.Evaluate()) * canSeePlayerConsideration.Evaluate();
        return decisionScore;
    }
}