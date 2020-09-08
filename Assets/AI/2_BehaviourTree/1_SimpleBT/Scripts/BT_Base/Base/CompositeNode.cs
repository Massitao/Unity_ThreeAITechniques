using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    [Header("Composite Node Properties")]
    public List<Node> childNodes;

    [Space(10)]

    [SerializeField] protected Node currentChildNode;
    protected int currentChildNodeIndex;
}