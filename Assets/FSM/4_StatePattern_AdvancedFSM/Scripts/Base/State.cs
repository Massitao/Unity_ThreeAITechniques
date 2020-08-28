using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}

public abstract class State : MonoBehaviour, IState
{
    [Header("State Name")]
    public string stateMachineName;

    [Header("State Properties")]
    [SerializeField] protected SubStateMachine parentSubStateMachine;
    [SerializeField] protected List<StateTransition> transitions;


    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }

    public State CheckTransitions()
    {
        foreach (StateTransition transition in transitions)
        {
            if (transition.CanTransition())
            {
                return transition.nextState;
            }
        }

        return null;
    }
    public SubStateMachine GetParentSubStateMachine()
    {
        return parentSubStateMachine;
    }
}