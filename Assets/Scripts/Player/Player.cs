using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private float totalGold = 0;

    [Header("Player Stats")]
    public float playerMaxHealth = 100f; // Valoarea setată în Inspectorul Player-ului
    public PlayerStatsManager statsManager;

    [Header("Events")]
    public UnityEvent<float> OnGoldChanged;

    void Awake()
    {
    statsManager = GetComponent<PlayerStatsManager>();
    
    // Safety check: If it's missing, add it
    if (statsManager == null) 
    {
        statsManager = gameObject.AddComponent<PlayerStatsManager>();
    }
    if (Instance == null)
    {
        Instance = this;
    }
}

    public void AddGold(float amount)
    {
        totalGold += amount;
        Debug.Log($"Gold Collected! Total: {totalGold}");
        OnGoldChanged?.Invoke(totalGold);
    }

    void Start()
    {
        // Player-ul își inițializează singur componenta Health
        Health myHealth = GetComponent<Health>();
        if (myHealth != null)
        {
            myHealth.InitializeHealth(playerMaxHealth);
        }
    }
}