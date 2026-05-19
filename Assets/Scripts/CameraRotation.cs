using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _sens = 0.2f;
    private float _rotateAngle = 0f;
    [SerializeField] private bool _vInverted = true;
    void Update()
    {
        float mouseY = Mouse.current.delta.ReadValue().y * _sens;
        _rotateAngle += mouseY;
        _rotateAngle = Mathf.Clamp(_rotateAngle, -90, 90);

        transform.localRotation = Quaternion.Euler(_vInverted ? -_rotateAngle : _rotateAngle,
                                                   transform.rotation.y, transform.rotation.z);
    }

    public float GetSens() => _sens;
}
