using UnityEngine;

[System.Serializable]
public abstract class Node : MonoBehaviour
{
    [Header("Debug Mode")]
    public bool debug;

    public abstract void Initialize();
    public abstract NodeStates Process();
}