using UnityEngine;
using UnityEngine.AI;

public class ShootNode : Node
{
    private readonly NavMeshAgent _agent;
    private readonly EnemyAI _ai;

    public ShootNode(NavMeshAgent agent, EnemyAI ai)
    {
        _agent = agent;
        _ai = ai;
    }

    public override NodeState Evaluate()
    {
        _agent.isStopped = true;
        _ai.SetColor(Color.green);
        return NodeState.Running;
    }
}