using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private CharacterController _characterController;
    private InputAction _moveAction;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravityForce = 1f;
    private float _gravity = 0f;
    [SerializeField] private float _jumpForce = 3f;
    private InputAction _jumpAction;
    private bool _inJump = false;
    private CameraRotation _cameraRotation;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _cameraRotation = Camera.main.GetComponent<CameraRotation>();
    }

    void Update()
    {
        Vector2 moveVector = _moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveVector.x, 0, moveVector.y);
        Vector3 moveVelocity = transform.TransformDirection(moveDirection);

        if (_characterController.isGrounded)
        {
            _inJump = false;
            _gravity = 0f;
        }
        else
        {
            _gravity -= _gravityForce * Time.deltaTime;
        }

        if (_jumpAction.WasPressedThisFrame() && !_inJump)
        {
            _gravity = _jumpForce;
            _inJump = true;
        }

        Vector3 finalMove = new Vector3(moveVelocity.x * _speed * Time.deltaTime,
                                        _gravity,
                                        moveVelocity.z * _speed * Time.deltaTime);
        _characterController.Move(finalMove);

        float mouseX = Mouse.current.delta.ReadValue().x * _cameraRotation.GetSens();
        transform.Rotate(0, mouseX, 0);
    }
}
