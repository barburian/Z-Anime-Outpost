using UnityEngine;
using UnityEngine.Events; // Necesar pentru UI (Lifebar) pe viitor

public class Health : MonoBehaviour
{
    [Header("Setări")]
    private float maxHealth ;
    private float currentHealth;

    [Header("Events (Opțional)")]
    public UnityEvent OnDeath; // Putem lega sunete sau efecte aici din Inspector
    public UnityEvent<float> OnDamageTaken; // Putem lega bara de viață aici

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void InitializeHealth(float healthValue)
    {
        maxHealth = healthValue;
        currentHealth = healthValue;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} a primit {amount} damage. Viață rămasă: {currentHealth}");

        // Invocăm evenimentul (pentru bara de viață, flash roșu, etc.)
        OnDamageTaken?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();

        // Logica diferită pentru Player vs Inamic
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("GAME OVER!");
            // Aici ai opri timpul: Time.timeScale = 0;
            // Sau ai afișa meniul de "You Died"
            // Nu distrugem player-ul imediat, altfel stricăm camera și scripturile
            gameObject.SetActive(false); 
        }
        else
        {
            // Este inamic
            Destroy(gameObject);
        }
    }
}