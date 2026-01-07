using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _collisionDamage = 10f;
    [SerializeField] private HealthSystem _health;

    private void Awake()
    {
        if (_health == null) _health = GetComponent<HealthSystem>();
        if (_health != null) _health.OnDeath.AddListener(() => Destroy(gameObject));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_collisionDamage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(_collisionDamage);
        }
    }
}