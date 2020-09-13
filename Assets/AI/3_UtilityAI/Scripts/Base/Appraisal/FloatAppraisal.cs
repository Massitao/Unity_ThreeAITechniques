using UnityEngine;

public class FloatAppraisal : Appraisal
{
    [Header("DEBUG: Float MinMax Values")]
    [SerializeField] protected float minValue;
    [SerializeField] protected float maxValue;

    public void SetStartFloatAppraisal(float min, float max, float currentValue)
    {
        minValue = min;
        maxValue = max;

        SetFloatAppraisal(currentValue);
    }
    public void SetFloatAppraisal(float currentValue)
    {
        score = Mathf.Clamp(currentValue, minValue, maxValue);
        normalizedScore = Mathf.InverseLerp(minValue, maxValue, currentValue);
    }
}