using System.Collections;
using UnityEngine;

public class BehaviourTree_Simple : MonoBehaviour
{
    [Header("Behaviour Tree Initial Node")]
    [SerializeField] protected Node initialNode;

    [Header("Behaviour Tree Update Coroutine")]
    protected Coroutine btCoroutine;
    protected bool active => btCoroutine != null;


    #region Behaviour Tree Methods
    #region Initialization / Exit Methods
    // Start up BT Coroutine
    public virtual void InitializeBT()
    {
        if (btCoroutine != null)
        {
            Debug.LogError($"BT is already running! Can't initialize again.");
            return;
        }

        if (initialNode == null)
        {
            Debug.LogError($"No Initial Node has been set! Can't initialize.");
            return;
        }

        btCoroutine = StartCoroutine(BTUpdate());
    }


    // Exit BT: stop Update Coroutine
    public virtual void ExitBT()
    {
        if (btCoroutine == null)
        {
            Debug.LogError($"BT is not running! Can't exit if it's not active.");
            return;
        }

        StopCoroutine(btCoroutine);
        btCoroutine = null;
    }
    #endregion

    #region Update Coroutine
    // Called once per frame. Can change tick time
    protected IEnumerator BTUpdate()
    {
        initialNode.Initialize();

        while (true)
        {
            NodeStates initialNodeState = initialNode.Process();

            if (initialNodeState != NodeStates.Running)
            {
                initialNode.Initialize();

                yield return BTUpdate();
            }

            yield return null;
        }
    }
    #endregion
    #endregion
}