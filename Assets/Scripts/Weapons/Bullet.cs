using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage; // Cât damage dă acest glonț specific
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
        
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); 
            
            Destroy(gameObject);
        }
       
    }
}