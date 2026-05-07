using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private float totalGold = 0;

    [Header("Level System")]
    public int currentLevel = 1;
    public float currentXP = 0;
    public float xpToLevelUp = 100f;

    [Header("Player Stats")]
    public float playerMaxHealth = 100f; // Valoarea setată în Inspectorul Player-ului
    public PlayerStatsManager statsManager;

    [Header("Events")]
    public UnityEvent<float> OnGoldChanged;
    public UnityEvent<float, float> OnXPChanged;
    public UnityEvent<int> OnLevelUp;

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
        OnGoldChanged?.Invoke(totalGold);
    }

    public void AddXP(float amount)
    {
        currentXP += amount;

        while (currentXP >= xpToLevelUp)
            LevelUp();

        OnXPChanged?.Invoke(currentXP, xpToLevelUp);
    }

    private void LevelUp()
    {
        currentXP -= xpToLevelUp;
        currentLevel++;
        OnLevelUp?.Invoke(currentLevel);
    }

    void Start()
    {
        // Player-ul își inițializează singur componenta Health
        Health myHealth = GetComponent<Health>();
        if (myHealth != null)
        {
            myHealth.InitializeHealth(playerMaxHealth);
        }
        OnXPChanged?.Invoke(currentXP, xpToLevelUp);
    }
}