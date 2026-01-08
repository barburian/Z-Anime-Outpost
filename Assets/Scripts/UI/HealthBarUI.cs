using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _healthBarFill;

    private void Start() 
    {
        // 1. Verificăm dacă Player-ul există
        if (Player.Instance != null)
        {
            // 2. Căutăm componenta Health de pe Player
            Health playerHealth = Player.Instance.GetComponent<Health>();
            
            if (playerHealth != null)
            {
                // 3. Ne ABONĂM la evenimentul de damage
                // Când player-ul pățește ceva, acest script va fi notificat automat
                playerHealth.OnDamageTaken.AddListener(UpdateHealthBar);
            }
        }
    }

    // Este important să ne dezabonăm când obiectul este distrus, ca să nu avem erori
    private void OnDestroy()
    {
        if (Player.Instance != null)
        {
            Health playerHealth = Player.Instance.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.OnDamageTaken.RemoveListener(UpdateHealthBar);
            }
        }
    }

    // Această funcție va fi apelată automat de Player când ia damage.
    // healthPercentage va fi un număr între 0.0 (gol) și 1.0 (plin).
    public void UpdateHealthBar(float healthPercentage)
    {
        _healthBarFill.fillAmount = healthPercentage;
    }
}