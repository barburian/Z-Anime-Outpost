using UnityEngine;
using System.Collections.Generic;
public class PlayerStatsManager:MonoBehaviour
{
    // Dictionaries to hold our current modifiers
    private Dictionary<StatType, float> _flatBonuses = new Dictionary<StatType, float>();
    private Dictionary<StatType, float> _multipliers = new Dictionary<StatType, float>();

    void Awake()
    {
        // Initialize dictionaries to avoid errors
        foreach (StatType stat in System.Enum.GetValues(typeof(StatType)))
        {
            _flatBonuses[stat] = 0f;
            _multipliers[stat] = 1f; // Base multiplier is 1 (100%)
        }
    }

    public void AddItem(ItemData item)
    {   
        if (item == null) return;

        Debug.Log($"Applying Item: {item.itemName}");

        foreach (var mod in item.modifiers)
        {   Debug.Log($"Applying Modifier: {mod.statType} with value {mod.value}");
            if (mod.modificationType == ModificationType.Flat)
            {   
                _flatBonuses[mod.statType] += mod.value;
                
            }
            else if (mod.modificationType == ModificationType.Multiplier)
            {
                // If the value is 0.1 (10%), we add it to the base 1.0
                _multipliers[mod.statType] += mod.value;
            }
        }
        
        // Special case: Update Max Health immediately if changed
        if (Player.Instance != null)
        {
           // Logic to heal player or increase max health bar could go here
        }
    }

    // --- Calculator Methods ---

    public float GetStatValue(float baseValue, StatType type)
    {
        // Formula: (Base + Flat) * Multiplier
        // Example: (10 Dmg + 5 Flat) * 1.5 Multiplier = 22.5
        float flat = _flatBonuses.ContainsKey(type) ? _flatBonuses[type] : 0f;
        float mult = _multipliers.ContainsKey(type) ? _multipliers[type] : 1f;

        return (baseValue + flat) * mult;
    }

    // Specialized Getter for FireRate (since logically, for Cooldown, a multiplier > 1 should make it faster, meaning lower cooldown)
    // Assuming FireRate in your data means "Time between shots" (Cooldown)
    public float GetFireRate(float baseCooldown)
    {
        float mult = _multipliers.ContainsKey(StatType.FireRate) ? _multipliers[StatType.FireRate] : 1f;
        // If I have +50% fire rate (mult = 1.5), the cooldown should be smaller.
        // Formula: Base / Multiplier
        return baseCooldown / mult;
    }
}
