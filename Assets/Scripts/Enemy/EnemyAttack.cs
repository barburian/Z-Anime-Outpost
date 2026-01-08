using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float damageAmount ;
    [SerializeField] private float hitInterval = 1f; // Dă damage o dată pe secundă
    private float nextHitTime = 0f;

    public void SetDamage(float damageFromData)
    {
        damageAmount = damageFromData;
    }
    // Folosim OnCollisionStay2D pentru că inamicul "împinge" player-ul
    // (Asigură-te că Inamicul NU are "Is Trigger" pe Collider-ul principal)
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verificăm dacă a trecut timpul de așteptare între lovituri
            if (Time.time >= nextHitTime)
            {
                // Căutăm viața player-ului
                Health playerHealth = collision.gameObject.GetComponent<Health>();
                
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    
                    // Resetăm cronometrul
                    nextHitTime = Time.time + hitInterval;
                }
            }
        }
    }
}
