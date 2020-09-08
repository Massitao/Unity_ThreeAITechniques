using UnityEngine;

[System.Serializable]
public abstract class Node_Blackboard : MonoBehaviour
{
    [Header("Debug Mode")]
    public bool debug;

    public abstract void Initialize(BlackboardBase bb);
    public abstract NodeStates Process(BlackboardBase bb);
}