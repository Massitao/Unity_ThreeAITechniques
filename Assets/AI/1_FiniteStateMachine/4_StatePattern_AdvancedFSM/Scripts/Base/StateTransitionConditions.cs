using UnityEngine;

namespace StatePattern_FSM
{
    [System.Serializable]
    public abstract class StateTransitionConditions : MonoBehaviour
    {
        public abstract bool IsMet();
    }
}