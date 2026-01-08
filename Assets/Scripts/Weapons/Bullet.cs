using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage; 
    private Rigidbody2D rb;

    
    public void Setup(float weaponDamage, float bulletSpeed)
    {
        damage = weaponDamage;
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = transform.right * bulletSpeed; 

        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // Ignorăm Player-ul
        if (other.CompareTag("Player")) return;

        // Căutăm componenta Health pe obiectul lovit (Inamic)
        if (other.TryGetComponent(out Health enemyHealth))
        {
            // Dacă are viață, îi dăm damage-ul setat în glonț
            enemyHealth.TakeDamage(damage);
        }

        // Dacă lovim inamicul sau pereții, distrugem glonțul
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
       
    }
}