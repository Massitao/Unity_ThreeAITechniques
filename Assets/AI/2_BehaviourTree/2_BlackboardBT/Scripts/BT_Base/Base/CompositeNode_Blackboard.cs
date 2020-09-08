using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode_Blackboard : Node_Blackboard
{
    [Header("Composite Node Properties")]
    public List<Node_Blackboard> childNodes;

    [Space(10)]

    [SerializeField] protected Node_Blackboard currentChildNode;
    protected int currentChildNodeIndex;
}