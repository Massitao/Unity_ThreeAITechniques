using UnityEngine;

public class IntAppraisal : Appraisal
{
    [Header("DEBUG: Int MinMax Values")]
    [SerializeField] protected int minValue;
    [SerializeField] protected int maxValue;

    public void SetStartFloatAppraisal(int min, int max, int currentValue)
    {
        minValue = min;
        maxValue = max;

        SetFloatAppraisal(currentValue);
    }
    public void SetFloatAppraisal(int currentValue)
    {
        score = Mathf.Clamp(currentValue, minValue, maxValue);
        normalizedScore = Mathf.InverseLerp(minValue, maxValue, currentValue);
    }
}