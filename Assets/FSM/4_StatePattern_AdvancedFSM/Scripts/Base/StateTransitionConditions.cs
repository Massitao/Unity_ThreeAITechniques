using UnityEngine;

[System.Serializable]
public abstract class StateTransitionConditions : MonoBehaviour
{
    public abstract bool IsMet();
}