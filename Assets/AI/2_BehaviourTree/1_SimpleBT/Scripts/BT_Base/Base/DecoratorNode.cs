using UnityEngine;

public abstract class DecoratorNode : Node
{
    [Header("Composite Node Properties")]
    public Node childNode;
}