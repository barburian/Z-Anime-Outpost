using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _healthBarFill;

    private HealthSystem _playerHealth;

    private void Start() 
    {
        if (Player.Instance != null)
        {
            _playerHealth = Player.Instance.Health;
            
            _playerHealth.OnHealthChanged.AddListener(UpdateHealthBar);
            
            UpdateHealthBar();
        }
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged.RemoveListener(UpdateHealthBar);
        }
    }

    private void UpdateHealthBar()
    {
        if (_playerHealth == null) return;
        
        float fillPercentage = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
        _healthBarFill.fillAmount = fillPercentage;
    }
}
