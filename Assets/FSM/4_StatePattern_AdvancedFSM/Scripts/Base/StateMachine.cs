using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class StateMachine : MonoBehaviour
{
    [Header("State Machine Name")]
    public string stateMachineName;

    [Header("State Machine Initial State")]
    [SerializeField] protected State initialState;

    [Header("State Machine Current Status")]
    [SerializeField] protected SubStateMachine currentSubStateMachine;
    [SerializeField] protected State currentState;
    protected bool active => fsmCoroutine != null;

    [Header("State Machine Ownership")]
    [SerializeField] protected List<SubStateMachine> allSubStateMachines;
    [SerializeField] protected List<SubStateMachine> allStates;

    [Header("State Machine Update Coroutine")]
    protected Coroutine fsmCoroutine;


    #region Mono Methods
    // Awake is called when the script instance is being loaded.
    protected abstract void Awake();

    // Start is called before the first frame update
    protected abstract void Start();
    #endregion

    #region State Machine Methods
    #region Initialization / Exit Methods
    // Start up FSM, setting it with an initial state
    public virtual void InitializeFSM(bool reset)
    {
        if (fsmCoroutine != null)
        {
            Debug.LogError($"FSM is already running! Can't initialize again.");
            return;
        }

        if (initialState == null)
        {
            Debug.LogError($"No Initial State has been set! Can't initialize.");
            return;
        }

        if (reset) ResetValueFSM();

        SetState(initialState);
        fsmCoroutine = StartCoroutine(FSMUpdate());
    }

    // Start up FSM, setting it with a custom state
    public virtual void InitializeCustomStateFSM(State customState, bool reset)
    {
        if (fsmCoroutine != null)
        {
            Debug.LogError($"FSM is already running! Can't initialize again.");
            return;
        }

        if (customState == null)
        {
            Debug.LogError($"No Custom State detected! Can't initialize.");
            return;
        }

        if (reset) ResetValueFSM();

        SetState(customState);
        fsmCoroutine = StartCoroutine(FSMUpdate());
    }


    // Clean / Reset variable values if needed
    public abstract void ResetValueFSM();


    // Exit FSM, exit current state and stop Update Coroutine
    public virtual void ExitFSM()
    {
        if (fsmCoroutine == null)
        {
            Debug.LogError($"FSM is not running! Can't exit if it's not active.");
            return;
        }

        currentState?.Exit();

        currentSubStateMachine = null;
        currentState = null;

        StopCoroutine(fsmCoroutine);
        fsmCoroutine = null;
    }
    #endregion

    #region Update Coroutine
    // Update is called once per frame
    protected IEnumerator FSMUpdate()
    {
        while (true)
        {
            State subStateMachineToTransition = currentSubStateMachine?.CheckTransitions();
            if (subStateMachineToTransition != null)
            {
                SetState(subStateMachineToTransition);
                break;
            }
            else
            {
                subStateMachineToTransition?.Execute();
            }

            State stateToTransition = currentState?.CheckTransitions();
            if (stateToTransition != null)
            {
                SetState(stateToTransition);
                break;
            }
            else
            {
                stateToTransition?.Execute();
            }

            yield return null;
        }
    }
    #endregion


    #region Get / Set State Methods
    public State GetState()
    {
        return currentState;
    }
    public void SetState(State newState)
    {
        if (newState != null)
        {
            if (newState is SubStateMachine)
            {
                currentSubStateMachine = newState as SubStateMachine;
                currentSubStateMachine?.Enter();
            }
            else
            {
                StackSubStateMachines();

                currentState?.Exit();
                currentState = newState;
                currentState?.Enter();
            }
        }
        else
        {
            Debug.LogError($"New State is null! Returning to Initial State.");

            SetState(initialState);
        }
    }

    public SubStateMachine GetSubStateMachine()
    {
        return currentSubStateMachine;
    }
    public List<SubStateMachine> GetAllActiveSubStateMachines()
    {
        return allSubStateMachines;
    }
    #endregion

    #region SubState Finder Method
    public virtual void StackSubStateMachines()
    {
        List<SubStateMachine> subStatesFound = new List<SubStateMachine>();
        bool foundParent = false;

        State nextState = currentState;
        SubStateMachine newlyFoundSubStateMachine = null;

        while (!foundParent)
        {
            newlyFoundSubStateMachine = nextState.GetParentSubStateMachine();

            if (newlyFoundSubStateMachine != null)
            {
                if (subStatesFound.Contains(newlyFoundSubStateMachine))
                {
                    Debug.LogError($"Stack Overflow! Looping same SubStateMachines. Debug Parents here: {currentState}");
                    ExitFSM();
                    return;
                }

                subStatesFound.Add(newlyFoundSubStateMachine);
                nextState = newlyFoundSubStateMachine;
                continue;
            }
            else
            {
                foundParent = true;
            }        
        }

        subStatesFound.Reverse();
        allSubStateMachines = subStatesFound;
        currentSubStateMachine = allSubStateMachines[allSubStateMachines.Count - 1];
    }
    #endregion
    #endregion
}