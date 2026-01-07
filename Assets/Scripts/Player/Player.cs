using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    [SerializeField] private HealthSystem _health;
    public HealthSystem Health 
    {
        get {
            if (_health == null) _health = GetComponent<HealthSystem>();
            return _health;
        }
    }

    private void Awake() 
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        
        if (Health != null)
        {
            Health.OnDeath.AddListener(HandleDeath);
        }
    }

    private void HandleDeath()
    {
        Debug.Log("Player Died");
        gameObject.SetActive(false); 
    }
}
