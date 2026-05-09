using System.Collections.Generic;
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
    [SerializeField] private PlayerData _playerData;
    public PlayerStatsManager statsManager;
    private Health _health;

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

    _playerData?.InitRuntimeStats();
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
        _health = GetComponent<Health>();
        if (_health != null)
        {
            float effectiveMax = statsManager.GetStatValue(_playerData.stats.health, StatType.PlayerHealth);
            _health.InitializeHealth(effectiveMax);
            _playerData.runtimeStats.health = effectiveMax;
        }
        statsManager.OnStatsChanged.AddListener(OnStatsChanged);
        OnXPChanged?.Invoke(currentXP, xpToLevelUp);
    }

    void OnDestroy()
    {
        if (statsManager != null)
            statsManager.OnStatsChanged.RemoveListener(OnStatsChanged);
    }

    public float GetEffectiveArmor()
    {
        return statsManager.GetStatValue(_playerData.stats.armor, StatType.PlayerArmor);
    }

    private void OnStatsChanged(List<StatType> changed)
    {
        if (changed.Contains(StatType.PlayerHealth))
        {
            float newMax = statsManager.GetStatValue(_playerData.stats.health, StatType.PlayerHealth);
            if (_health != null)
            {
                _playerData.runtimeStats.health = newMax;
                float delta = newMax - _health.MaxHealth;
                if (delta != 0)
                    _health.AddMaxHealth(delta, true);
            }
        }

        if (changed.Contains(StatType.PlayerArmor))
        {
            _playerData.runtimeStats.armor = GetEffectiveArmor();
        }
    }
}