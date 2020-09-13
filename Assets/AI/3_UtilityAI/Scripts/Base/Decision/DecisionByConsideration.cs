using System.Collections.Generic;
using UnityEngine;

public class DecisionByConsideration : Decision
{
    [Header("Decision Considerations")]
    [SerializeField] protected List<Consideration> considerations;

    public override float Evaluate()
    {
        decisionScore = 0f;

        for (int i = 0; i < considerations.Count; i++)
        {
            decisionScore += considerations[i].Evaluate();
        }

        decisionScore /= considerations.Count;
        return decisionScore;
    }
}