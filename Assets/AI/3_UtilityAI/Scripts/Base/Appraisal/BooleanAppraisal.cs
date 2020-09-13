using UnityEngine;

public class BooleanAppraisal : Appraisal
{
    [Header("DEBUG: Boolean Appraisal Score")]
    [SerializeField] protected bool boolScore;

    public void SetBoolAppraisal(bool newBool)
    {
        boolScore = newBool;

        score = (boolScore) ? 1f : 0f;
        normalizedScore = score;
    }
}