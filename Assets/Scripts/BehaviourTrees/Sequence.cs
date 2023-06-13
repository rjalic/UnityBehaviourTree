using System.Collections.Generic;

public class Sequence : Node
{
    private List<Node> _nodes;

    public Sequence(List<Node> nodes)
    {
        _nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        var isAnyNodeRunning = false;
        foreach (var node in _nodes)
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.Success:
                    break;
                case NodeState.Failure:
                    _nodeState = NodeState.Failure;
                    return _nodeState;
            }

        _nodeState = isAnyNodeRunning ? NodeState.Running : NodeState.Success;
        return _nodeState;
    }
}