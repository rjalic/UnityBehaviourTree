using UnityEngine;

public class RangeNode : Node
{
    private readonly Transform _origin;
    private readonly float _range;
    private readonly Transform _target;

    public RangeNode(float range, Transform target, Transform origin)
    {
        _range = range;
        _target = target;
        _origin = origin;
    }

    public override NodeState Evaluate()
    {
        var distance = Vector3.Distance(_target.position, _origin.position);
        return distance <= _range ? NodeState.Success : NodeState.Failure;
    }
}