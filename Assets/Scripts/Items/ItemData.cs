// FILE: Assets/Scripts/Items/ItemData.cs
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    // Player Stats
    PlayerHealth,
    PlayerMoveSpeed,
    PlayerArmor,
    
    // Weapon Stats (Global)
    GlobalDamage,
    FireRate,       // Lower is faster usually, or Higher is faster depending on logic. We will assume Multiplier.
    BulletSpeed,
    WeaponRange,

    // Enemy Stats (Curse)
    EnemyHealthMultiplier,
    EnemyDamageMultiplier
}

public enum ModificationType
{
    Flat,       // Adds directly (e.g., +10 HP)
    Multiplier  // Multiplies (e.g., +0.10 is +10%)
}

[System.Serializable]
public class StatModifier
{
    public StatType statType;
    public ModificationType modificationType;
    public float value; // e.g., 10 for Flat, 0.2 for 20%
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Identity")]
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;

    [Header("Effects")]
    public List<StatModifier> modifiers;
} 