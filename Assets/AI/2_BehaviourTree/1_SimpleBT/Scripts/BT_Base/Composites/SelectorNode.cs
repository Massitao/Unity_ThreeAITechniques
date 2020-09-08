using UnityEngine;

public class SelectorNode : CompositeNode
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
                    currentChildNode.Initialize();

                    return Process();
                }
        }
        if (debug) Debug.Log($"Selector still running: {gameObject.name}", gameObject);

        return NodeStates.Running;
    }
}