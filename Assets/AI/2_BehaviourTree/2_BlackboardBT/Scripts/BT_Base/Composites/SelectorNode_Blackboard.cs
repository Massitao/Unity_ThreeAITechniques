using UnityEngine;

public class SelectorNode_Blackboard : CompositeNode_Blackboard
{
    public override void Initialize(BlackboardBase bb)
    {
        currentChildNodeIndex = 0;
        currentChildNode = childNodes[currentChildNodeIndex];

        currentChildNode.Initialize(bb);
    }

    public override NodeStates Process(BlackboardBase bb)
    {
        switch (currentChildNode.Process(bb))
        {
            case NodeStates.Success:
                if (debug) Debug.Log($"Selector Complete!: {gameObject.name}", gameObject);

                return NodeStates.Success;

            case NodeStates.Failure:
                currentChildNodeIndex++;

                if (currentChildNodeIndex >= childNodes.Count)
                {
                    if (debug) Debug.Log($"Selector Failed!: {gameObject.name}", gameObject);

                    return NodeStates.Failure;
                }
                else
                {
                    if (debug) Debug.Log($"Child {currentChildNode.name} failed, continuing Selector {gameObject.name}", gameObject);

                    currentChildNode = childNodes[currentChildNodeIndex];
                    currentChildNode.Initialize(bb);

                    return Process(bb);
                }
        }
        if (debug) Debug.Log($"Selector still running: {gameObject.name}", gameObject);

        return NodeStates.Running;
    }
}