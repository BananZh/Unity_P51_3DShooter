using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMoveNavMesh : MonoBehaviour
{
    [SerializeField] private bool _isChasing = false;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _chaseDistance = 10f;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckChasing();
        if (_isChasing)
        {
            _navMeshAgent.SetDestination(_targetTransform.position);
        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
        }
    }


    void CheckChasing()
    {
        _isChasing = Vector3.Distance(transform.position, _targetTransform.position) <= _chaseDistance;
    }
}
