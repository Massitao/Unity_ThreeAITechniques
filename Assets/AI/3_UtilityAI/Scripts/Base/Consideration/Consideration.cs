using System.Collections.Generic;
using UnityEngine;

public class Consideration : MonoBehaviour
{
    [Header("DEBUG: Consideration Score")]
    [SerializeField] protected float considerationScore;

    [Header("Consideration Evaluation")]
    [SerializeField] [Range(0f, 1f)] protected float considerationMultiplier = 1f;
    [SerializeField] protected AnimationCurve considerationEvaluation = new AnimationCurve(new Keyframe[2] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });

    [Header("Appraisal & Other Consideration Appraisals")]
    [SerializeField] protected List<Consideration> otherConsiderationAppraisals;
    [Space(10)]
    [SerializeField] protected Appraisal thisConsiderationAppraisal;


    public void SetConsiderationWeight(float weight)
    {
        considerationMultiplier = Mathf.Clamp01(weight);
    }
    public float Evaluate()
    {
        considerationScore = 0f;
        int ammountOfAppraisals = 0;

        for (int i = 0; i < otherConsiderationAppraisals.Count; i++)
        {
            considerationScore += otherConsiderationAppraisals[i].Evaluate();
            ammountOfAppraisals++;
        }

        if (thisConsiderationAppraisal != null)
        {
            considerationScore += thisConsiderationAppraisal.Evaluate();
            ammountOfAppraisals++;
        }

        considerationScore = considerationEvaluation.Evaluate(considerationScore / ammountOfAppraisals) * considerationMultiplier;
        return considerationScore;
    }
}