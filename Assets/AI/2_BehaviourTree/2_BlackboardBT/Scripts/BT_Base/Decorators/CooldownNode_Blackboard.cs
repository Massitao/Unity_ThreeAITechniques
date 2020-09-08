using System.Collections;
using UnityEngine;

public class CooldownNode_Blackboard : DecoratorNode_Blackboard
{
    [Header("Node Properties")]
    public float cooldownTime;
    public bool onCooldown => cooldownCoroutine != null;

    protected Coroutine cooldownCoroutine;


    public override void Initialize(BlackboardBase bb)
    {
        if (!onCooldown) childNode.Initialize(bb);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        if (!onCooldown)
        {
            NodeStates childNodeState = childNode.Process(bb);

            if (childNodeState != NodeStates.Running)
            {
                StartCooldown();
            }

            return childNodeState;
        }
        else
        {
            return NodeStates.Failure;
        }
    }

    protected void StartCooldown()
    {
        if (cooldownCoroutine != null)
        {
            Debug.LogError($"Starting cooldown when is already on cooldown?");
            StopCoroutine(cooldownCoroutine);
        }

        cooldownCoroutine = StartCoroutine(Cooldown());
    }
    protected void EndCooldown()
    {
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
            cooldownCoroutine = null;
        }
    }

    protected IEnumerator Cooldown()
    {
        for (float time = cooldownTime; time > 0f; time -= Time.deltaTime)
        {
            if (debug) Debug.Log($"Cooldown: {gameObject.name}. Timer is: {cooldownTime - time}. Cooldown Time is: {cooldownTime}", gameObject);

            yield return null;
        }

        if (debug) Debug.Log($"Cooldown: {gameObject.name} has finished!", gameObject);

        EndCooldown();
    }
}