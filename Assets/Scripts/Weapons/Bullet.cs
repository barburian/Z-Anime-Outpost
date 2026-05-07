using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    private float damage;
    private Rigidbody2D rb;
    private Coroutine _returnTimer;

    public void Setup(float weaponDamage, float bulletSpeed)
    {
        damage = weaponDamage;
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed;
        _returnTimer = StartCoroutine(ReturnAfterDelay(5f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        if (other.TryGetComponent(out IDamageable target))
            target.TakeDamage(damage);

        if (other.CompareTag("Enemy"))
            ReturnToPool();
    }

    private IEnumerator ReturnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (!gameObject.activeInHierarchy) return;
        if (_returnTimer != null) { StopCoroutine(_returnTimer); _returnTimer = null; }
        ObjectPool.Instance.Return(gameObject);
    }

    public void OnSpawn() { }

    public void OnDespawn()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }
}
