using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _iFrameDuration = 0.5f;
    
    private float _currentHealth;
    private float _lastDamageTime;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public UnityEvent OnHealthChanged;
    public UnityEvent OnDeath;

    private void Awake() => _currentHealth = _maxHealth;

    public void TakeDamage(float amount)
    {
        if (Time.time < _lastDamageTime + _iFrameDuration) return;

        _currentHealth = Mathf.Max(_currentHealth - amount, 0);
        _lastDamageTime = Time.time; 

        OnHealthChanged?.Invoke();
        
        if (_currentHealth <= 0) OnDeath?.Invoke();
    }
}