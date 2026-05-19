using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private Camera _cam1;
    private InputAction _shootAction;
    [SerializeField] private float _maxDistance = 1000f;
    [SerializeField] private float _damage = 35f;
    [SerializeField] private float _damageMult = 1f;
    [SerializeField] private float _headHitMult = 3f;
    [SerializeField] private float _handHitMult = 0.65f;
    [SerializeField] private float _legHitMult = 0.8f;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _fireRate = 0.1f;
    private float _fireTimer = 0f;

    void Start()
    {
        _cam1 = Camera.main;
        _shootAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        // if (_shootAction.WasPressedThisFrame())
        // {
        //     Shoot();
        // }
        _fireTimer += Time.deltaTime;
        if (_shootAction.IsPressed() && _fireTimer > _fireRate)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        _fireTimer = 0;
        _audioSource.Play();
        Ray ray = new Ray(_cam1.transform.position, _cam1.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxDistance))
        {
            if (hitInfo.transform.root.tag == "Enemy")
            {
                float damage = _damage;
                switch (hitInfo.transform.tag)
                {
                    case "Body":
                        print("shooted at Body");
                        damage *= _damageMult;
                        break;
                    case "Head":
                        print("shooted at Head");
                        damage *= _damageMult * _headHitMult;
                        break;
                    case "Hand":
                        print("shooted at Hand");
                        damage *= _handHitMult * _damageMult;
                        break;
                    case "Leg":
                        print("shooted at Leg");
                        damage *= _legHitMult * _damageMult;
                        break;
                }
                GiveDamage(damage, hitInfo.transform.root.gameObject);
            }
        }
    }

    void GiveDamage(float damage, GameObject enemy)
    {
        enemy.GetComponent<Enemy>().GetDamage(damage);
        print($"Given {damage} damage to {enemy.name}");
    }
}
