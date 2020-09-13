using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reasoner : MonoBehaviour
{
    [Header("List of Actions - Can set in Editor Inspector or Code")]
    public List<Decision> decisionsToEvaluate;

    [Header("DEBUG: Current Decision Values")]
    [SerializeField] protected Decision chosenDecision;
    [SerializeField] protected float chosenDecisionScore;

    [Header("Reasoner Evaluation Tick Time")]
    [SerializeField] protected float tickTime;
    protected Coroutine utilityAICoroutine;
    protected Coroutine utilityAIEvaluationCoroutine;
    protected bool active => utilityAICoroutine != null;

    [Header("Decision To Transition")]
    protected bool enteringNewDecision;
    protected Decision newDecision;
    protected float newDecisionScore;

    [Header("Decision Change Event")]
    public Action<string> OnDecisionChange;

    #region Utility AI Methods
    #region Initialization / Exit Methods
    // Start up Utility AI / Reasoner Coroutine
    public virtual void InitializeUtilityAI()
    {
        if (utilityAICoroutine != null)
        {
            Debug.LogError($"Utility AI / Reasoner is already running! Can't initialize again.");
            return;
        }

        if (chosenDecision == null)
        {
            EvaluateDecisions();

            if (newDecision != null)
            {
                SetChosenDecision(newDecision, newDecisionScore);
            }
            else
            {
                Debug.LogError($"Utility AI / Reasoner evaluator has returned null! Can't initialize.");
                return;
            }
        }

        utilityAICoroutine = StartCoroutine(UtilityAIUpdate());
    }


    // Exit Utility AI / Reasoner: stop Update Coroutine
    public virtual void ExitUtilityAI()
    {
        if (utilityAICoroutine == null)
        {
            Debug.LogError($"Utility AI / Reasoner is not running! Can't exit if it's not active.");
            return;
        }

        StopCoroutine(utilityAICoroutine);
        StopCoroutine(utilityAIEvaluationCoroutine);
        utilityAICoroutine = null;
        utilityAIEvaluationCoroutine = null;
    }
    #endregion

    #region Update Coroutine
    // Called once per frame. Can change tick time
    protected IEnumerator UtilityAIUpdate()
    {
        utilityAIEvaluationCoroutine = StartCoroutine(UtilityAIEvaluation());

        while (true)
        {
            if (enteringNewDecision)
            {
                chosenDecision.actionToDoWhenEntering?.Invoke();
                enteringNewDecision = false;
            }

            bool? decisionActionComplete = chosenDecision.actionToDo?.Invoke();
            if (decisionActionComplete == null) Debug.Log($"No Event has been set for {chosenDecision}! Fix it!", chosenDecision.gameObject);

            if ((bool)decisionActionComplete)
            {
                StopCoroutine(utilityAIEvaluationCoroutine);
                utilityAIEvaluationCoroutine = null;

                EvaluateDecisions();
                SetChosenDecision(newDecision, newDecisionScore);

                utilityAIEvaluationCoroutine = StartCoroutine(UtilityAIEvaluation());
            }
            else
            {
                if (chosenDecision != newDecision && newDecision != null)
                {
                    if (chosenDecision.canInterruptAction)
                    {
                        chosenDecision.actionToDoIfInterrupted?.Invoke();
                        SetChosenDecision(newDecision, newDecisionScore);
                    }
                }
            }

            yield return null;
        }
    }
    // Called once per tick time
    protected IEnumerator UtilityAIEvaluation()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickTime);
            EvaluateDecisions();
        }
    }

    protected virtual void EvaluateDecisions()
    {
        Decision topDecision = null;
        float highestDecisionScore = 0f;
        float selectedDecisionScore = 0f;

        for (int i = 0; i < decisionsToEvaluate.Count; i++)
        {
            selectedDecisionScore = decisionsToEvaluate[i].Evaluate();

            if (selectedDecisionScore > highestDecisionScore)
            {
                highestDecisionScore = selectedDecisionScore;
                topDecision = decisionsToEvaluate[i];
            }
        }

        newDecision = topDecision;
        newDecisionScore = highestDecisionScore;
    }
    #endregion

    #region Set new Decision / Action
    protected virtual void SetChosenDecision(Decision newChosenDecision, float newChosenDecisionScore)
    {
        chosenDecision = newChosenDecision;
        chosenDecisionScore = newChosenDecisionScore;
        enteringNewDecision = true;

        OnDecisionChange?.Invoke(chosenDecision.decisionName);
    }
    #endregion
    #endregion
}