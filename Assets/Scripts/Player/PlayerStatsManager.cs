using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PlayerStatsManager : MonoBehaviour
{
    private Dictionary<StatType, float> _flatBonuses = new Dictionary<StatType, float>();
    private Dictionary<StatType, float> _multipliers = new Dictionary<StatType, float>();

    // Fires once after all modifiers from a single item are applied.
    // Payload = the list of stats that changed, so listeners can filter.
    public UnityEvent<List<StatType>> OnStatsChanged;

    void Awake()
    {
        foreach (StatType stat in System.Enum.GetValues(typeof(StatType)))
        {
            _flatBonuses[stat] = 0f;
            _multipliers[stat] = 1f;
        }
    }

    public void AddItem(ItemData item)
    {
        if (item == null) return;

        // Collect which stats this item touches (deduplicated)
        HashSet<StatType> changedStats = new HashSet<StatType>();

        foreach (var mod in item.modifiers)
        {
            if (mod.modificationType == ModificationType.Flat)
                _flatBonuses[mod.statType] += mod.value;
            else
                _multipliers[mod.statType] += mod.value;

            changedStats.Add(mod.statType);
        }

        // Fire ONCE after all modifiers applied, with the list of affected stats
        OnStatsChanged?.Invoke(new List<StatType>(changedStats));
    }

    public float GetStatValue(float baseValue, StatType type)
    {
        float flat = _flatBonuses.ContainsKey(type) ? _flatBonuses[type] : 0f;
        float mult = _multipliers.ContainsKey(type) ? _multipliers[type] : 1f;
        return (baseValue + flat) * mult;
    }

    public float GetFireRate(float baseCooldown)
    {
        float mult = _multipliers.ContainsKey(StatType.FireRate) ? _multipliers[StatType.FireRate] : 1f;
        return baseCooldown / mult;
    }
}