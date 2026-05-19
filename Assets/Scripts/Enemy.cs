using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _hp = 100f;
    void Start()
    {

    }
    void Update()
    {

    }

    public void GetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0f)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
