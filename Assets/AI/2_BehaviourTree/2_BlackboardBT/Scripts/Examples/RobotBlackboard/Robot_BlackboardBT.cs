using UnityEngine;

public class Robot_BlackboardBT : MonoBehaviour
{
    [Header("This script will initialize BT")]
    [SerializeField] private BehaviourTree_Blackboard behaviourTree;

    private void Start()
    {
        behaviourTree.InitializeBT();
    }
}