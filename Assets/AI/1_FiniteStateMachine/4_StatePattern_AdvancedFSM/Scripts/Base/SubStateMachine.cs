using UnityEngine;

namespace StatePattern_FSM
{
    public abstract class SubStateMachine : State
    {
        [Header("Sub State Machine Properties")]
        public State entryState;
    }
}