using System;
using UnityEngine;

public abstract class Decision : MonoBehaviour
{
    [Header("Decision / Action Name")]
    public string decisionName;

    [Header("DEBUG: Decision Score")]
    [SerializeField] protected float decisionScore;

    [Header("Can Interrupt this action?")]
    public bool canInterruptAction = true;

    [Header("Decision / Action Events")]
    public Action actionToDoWhenEntering;
    public Func<bool> actionToDo;
    public Action actionToDoIfInterrupted;

    public abstract float Evaluate();
}