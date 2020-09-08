using System.Collections.Generic;
using UnityEngine;

namespace StatePattern_FSM
{
    [System.Serializable]
    public class StateTransition
    {
        [Header("Transition Name")]
        [SerializeField] private string transitionName;

        [Header("State To Transition")]
        public State nextState;

        [Header("Transition Conditions")]
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
}