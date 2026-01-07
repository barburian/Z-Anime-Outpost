using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private Rigidbody2D _rb;

    public void Setup(float weaponDamage, float bulletSpeed)
    {
        _damage = weaponDamage;
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = transform.right * bulletSpeed; 
        
        CancelInvoke();
        Invoke(nameof(Deactivate), 5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable hitTarget))
        {
            hitTarget.TakeDamage(_damage);
            Deactivate();
        }
    }

    private void Deactivate() => gameObject.SetActive(false);
}
