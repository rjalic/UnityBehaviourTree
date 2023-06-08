public class Inverter : Node
{
    protected Node node;

    public Inverter(Node node)
    {
        this.node = node;
    }

    public override NodeState Evaluate()
    {
        switch (node.Evaluate())
        {
            case NodeState.Running:
                _nodeState = NodeState.Running;
                break;
            case NodeState.Success:
                _nodeState = NodeState.Failure;
                return _nodeState;
            case NodeState.Failure:
                _nodeState = NodeState.Success;
                return _nodeState;
        }

        return _nodeState;
    }
}