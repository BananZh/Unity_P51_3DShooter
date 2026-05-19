using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    private Vector3 _lastPos;
    [SerializeField] private Transform _TPPos;
    private InputAction TP;
    private InputAction TPBack;
    void Start()
    {
        TP = InputSystem.actions.FindAction("Teleport");
        TPBack = InputSystem.actions.FindAction("Teleport Back");
    }

    void Update()
    {
        if (TP.WasPressedThisFrame())
        {
            _lastPos = transform.position;
            GetComponent<CharacterController>().enabled = false;
            transform.position = _TPPos.position;
            print("TP");
            GetComponent<CharacterController>().enabled = true;
        }

        if (TPBack.WasPressedThisFrame())
        {
            GetComponent<CharacterController>().enabled = false;
            transform.position = _lastPos;
            print("TPBack");
            GetComponent<CharacterController>().enabled = true;
        }
    }
}
