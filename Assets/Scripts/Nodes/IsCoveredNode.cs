using UnityEngine;

public class IsCoveredNode : Node
{
    private readonly Transform _origin;
    private readonly Transform _target;

    public IsCoveredNode(Transform target, Transform origin)
    {
        _target = target;
        _origin = origin;
    }

    public override NodeState Evaluate()
    {
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _target.position - _origin.position, out hit))
            if (hit.collider.transform != _target)
                return NodeState.Success;

        return NodeState.Failure;
    }
}