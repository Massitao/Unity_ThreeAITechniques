using UnityEngine;

public class SequenceNode_Blackboard : CompositeNode_Blackboard
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
                currentChildNodeIndex++;

                if (currentChildNodeIndex >= childNodes.Count)
                {
                    if (debug) Debug.Log($"Sequence Complete!: {gameObject.name}", gameObject);

                    return NodeStates.Success;
                }
                else
                {
                    if (debug) Debug.Log($"Child {currentChildNode.name} succeded, continuing Sequence {gameObject.name}", gameObject);

                    currentChildNode = childNodes[currentChildNodeIndex];
                    currentChildNode.Initialize(bb);

                    return Process(bb);
                }

            case NodeStates.Failure:
                if (debug) Debug.Log($"Sequence Failed!: {gameObject.name}");

                return NodeStates.Failure;
        }
        if (debug) Debug.Log($"Sequence still running: {gameObject.name}", gameObject);

        return NodeStates.Running;
    }
}