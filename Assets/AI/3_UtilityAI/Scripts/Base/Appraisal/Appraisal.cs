using UnityEngine;

public abstract class Appraisal : MonoBehaviour
{
    [Header("DEBUG: Appraisal Score")]
    [SerializeField] protected float score;
    [SerializeField] protected float normalizedScore;

    public float Evaluate()
    {
        return normalizedScore;
    }
}