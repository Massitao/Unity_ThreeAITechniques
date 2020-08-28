using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateTransition
{
    public State nextState;
    [SerializeField] protected List<StateTransitionConditions> conditions;

    public bool CanTransition()
    {
        foreach (StateTransitionConditions condition in conditions)
        {
            if (!condition.IsMet())
            {
                return false;
            }
        }
        return true;
    }
}