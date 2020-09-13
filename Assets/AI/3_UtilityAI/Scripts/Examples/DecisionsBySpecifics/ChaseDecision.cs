using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseDecision : DecisionBySpecifics
{
    [Header("Chase Considerations")]
    [SerializeField] private Consideration canSeePlayerConsideration;
    [SerializeField] private Consideration playerIsFarConsideration;
    [SerializeField] private Decision healDecision;


    public override float Evaluate()
    {
        decisionScore = (playerIsFarConsideration.Evaluate() - healDecision.Evaluate()) * canSeePlayerConsideration.Evaluate();
        return decisionScore;
    }
}
