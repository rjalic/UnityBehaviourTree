public class HealthNode : Node
{
    private readonly EnemyAI _ai;
    private readonly float _threshold;

    public HealthNode(EnemyAI ai, float threshold)
    {
        _ai = ai;
        _threshold = threshold;
    }

    public override NodeState Evaluate()
    {
        return _ai.CurrentHealth <= _threshold ? NodeState.Success : NodeState.Failure;
    }
}