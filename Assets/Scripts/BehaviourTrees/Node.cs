[System.Serializable]
public abstract class Node
{
    protected NodeState _nodeState;

    public NodeState nodeState { get { return _nodeState; } }

    public abstract NodeState Evaluate();
}

public enum NodeState
{
    Running,
    Success,
    Failure,
}