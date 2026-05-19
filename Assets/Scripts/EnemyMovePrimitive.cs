using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovePrimitive : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private bool _isChasing = false;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _chaseDistance = 10f;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _gravityForce = 1f;
    private Vector3 _moveDirection;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CheckChasing();
        Vector3 chaseDirection = _isChasing ?
        (_targetTransform.position - transform.position).normalized : Vector3.zero;
        _moveDirection = new(chaseDirection.x, _moveDirection.y, chaseDirection.z);
        if (_isChasing) RotateOnTarget();

        Gravity();
        Vector3 moveVector = new(Time.deltaTime * _moveDirection.x * _speed,
                                 _moveDirection.y,
                                 Time.deltaTime * _moveDirection.z * _speed);
        _characterController.Move(moveVector);
    }

    void Gravity()
    {
        if (_characterController.isGrounded)
        {
            _moveDirection.y = 0f;
        }
        else
        {
            _moveDirection.y -= _gravityForce * Time.deltaTime;
        }
    }
    void RotateOnTarget()
    {
        Vector3 lookAtPos = new Vector3(_targetTransform.position.x,
                                            transform.position.y,
                                            _targetTransform.position.z);
        transform.LookAt(lookAtPos);
    }
    void CheckChasing()
    {
        _isChasing = Vector3.Distance(transform.position, _targetTransform.position) <= _chaseDistance;
    }
}
