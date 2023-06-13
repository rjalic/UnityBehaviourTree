using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    private readonly NavMeshAgent _agent;
    private readonly EnemyAI _ai;
    private readonly Transform _target;

    public ChaseNode(Transform target, NavMeshAgent agent, EnemyAI ai)
    {
        _target = target;
        _agent = agent;
        _ai = ai;
    }

    public override NodeState Evaluate()
    {
        _ai.SetColor(Color.yellow);
        var distance = Vector3.Distance(_target.position, _agent.transform.position);
        if (distance > 0.2f)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);
            return NodeState.Running;
        }

        _agent.isStopped = true;
        return NodeState.Success;
    }
}