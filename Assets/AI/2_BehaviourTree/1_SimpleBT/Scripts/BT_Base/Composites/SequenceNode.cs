using UnityEngine;

public class SequenceNode : CompositeNode
{
    public override void Initialize()
    {
        currentChildNodeIndex = 0;
        currentChildNode = childNodes[currentChildNodeIndex];

        currentChildNode.Initialize();
    }

    public override NodeStates Process()
    {
        switch (currentChildNode.Process())
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
                    currentChildNode.Initialize();

                    return Process();
                }

            case NodeStates.Failure:
                if (debug) Debug.Log($"Sequence Failed!: {gameObject.name}");

                return NodeStates.Failure;
        }
        if (debug) Debug.Log($"Sequence still running: {gameObject.name}", gameObject);

        return NodeStates.Running;
    }
}