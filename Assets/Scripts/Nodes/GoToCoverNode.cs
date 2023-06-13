using UnityEngine;
using UnityEngine.AI;

public class GoToCoverNode : Node
{
    private readonly NavMeshAgent _agent;
    private readonly EnemyAI _ai;

    public GoToCoverNode(NavMeshAgent agent, EnemyAI ai)
    {
        _agent = agent;
        _ai = ai;
    }

    public override NodeState Evaluate()
    {
        var coverSpot = _ai.GetBestCoverSpot();
        if (coverSpot == null) return NodeState.Failure;
        _ai.SetColor(Color.magenta);
        var distance = Vector3.Distance(coverSpot.position, _agent.transform.position);
        if (distance > 0.2f)
        {
            _agent.isStopped = false;
            _agent.SetDestination(coverSpot.position);
            return NodeState.Running;
        }

        _agent.isStopped = true;
        return NodeState.Success;
    }
}