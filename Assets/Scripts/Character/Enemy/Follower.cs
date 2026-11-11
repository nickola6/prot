
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Follower : MonoBehaviour
{
    private const float DestinationUpdateInterval = 0.1f;
    private const float DirectionThreshold = 0.04f;

    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _moveSpeedVariation = 0.2f;

    private NavMeshAgent _agent;
    private CharacterFacing _characterFacing;
    private Transform _player;
    private float _nextDestinationUpdateTime;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _characterFacing = GetComponentInChildren<CharacterFacing>(true);

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.autoBraking = false;
    }

    private void OnDisable()
    {
        _player = null;

        if (_agent.isOnNavMesh)
            _agent.ResetPath();
    }

    public void Initialize(Transform player)
    {
        _player = player;
        _agent.speed = Mathf.Max(0f, _moveSpeed + Random.Range(-_moveSpeedVariation, _moveSpeedVariation));
        _agent.avoidancePriority = Random.Range(0, 100);
        _nextDestinationUpdateTime = 0f;
    }

    private void Update()
    {
        if (_player == null || _agent.isOnNavMesh == false)
            return;

        if (Time.time >= _nextDestinationUpdateTime)
        {
            _agent.SetDestination(_player.position);
            _nextDestinationUpdateTime = Time.time + DestinationUpdateInterval;
        }

        Vector2 direction = _agent.velocity;

        if (direction.sqrMagnitude > DirectionThreshold * DirectionThreshold)
            _characterFacing.SetDirection(direction);
    }
}
