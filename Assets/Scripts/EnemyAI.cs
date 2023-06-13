using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestorationRate;
    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Cover[] availableCovers;

    private NavMeshAgent _agent;
    private Transform _bestCoverSpot;
    private float _currentHealth;
    private Material _material;
    private Node _topNode;

    public float CurrentHealth
    {
        get => _currentHealth;
        private set => _currentHealth = Mathf.Clamp(value, 0, _currentHealth);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        _currentHealth = startingHealth;
        ConstructBehaviourTree();
    }

    private void Update()
    {
        _topNode.Evaluate();
        if (_topNode.NodeState == NodeState.Failure)
        {
            SetColor(Color.red);
            _agent.isStopped = true;
        }

        _currentHealth += Time.deltaTime * healthRestorationRate;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    private void ConstructBehaviourTree()
    {
        var isCoverAvailableNode = new IsCoverAvailable(availableCovers, playerTransform, this);
        var goToCoverNode = new GoToCoverNode(_agent, this);
        var healthNode = new HealthNode(this, lowHealthThreshold);
        var isCoveredNode = new IsCoveredNode(playerTransform, transform);
        var chaseNode = new ChaseNode(playerTransform, _agent, this);
        var chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        var shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);
        var shootNode = new ShootNode(_agent, this);

        var chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        var shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });
        var goToCoverSequence = new Sequence(new List<Node> { isCoverAvailableNode, goToCoverNode });
        var findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        var tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        var mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });
        _topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }

    public void SetBestCoverSpot(Transform bestCoverSpot)
    {
        _bestCoverSpot = bestCoverSpot;
    }

    public Transform GetBestCoverSpot()
    {
        return _bestCoverSpot;
    }
}