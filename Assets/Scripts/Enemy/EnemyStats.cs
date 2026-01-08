using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // Aici vine fișierul de date de la Spawner
    public void Setup(EnemyData data)
    {
        // 1. Setăm Grafica
        GetComponent<SpriteRenderer>().sprite = data.enemySprite;

        // 2. Setăm Viața (din întrebarea anterioară)
        var healthScript = GetComponent<Health>();
        if (healthScript != null)
        {
            healthScript.InitializeHealth(data.stats.health);
        }

        // 3. Setăm DAMAGE-UL (Asta ai cerut acum)
        var attackScript = GetComponent<EnemyAttack>();
        if (attackScript != null)
        {
            // Luăm valoarea 'damage' din ScriptableObject și o trimitem la scriptul de atac
            attackScript.SetDamage(data.stats.damage);
        }

        // 4. (Opțional) Setăm Viteza
        // var movementScript = GetComponent<EnemyMovement>();
        // if (movementScript != null) movementScript.SetSpeed(data.enemySpeed);
    }
}