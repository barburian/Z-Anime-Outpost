using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Identity")]
    public string characterName;
    public Sprite characterSprite;
    public GameObject characterPrefab;

    [System.Serializable]
    public class Stats
    {
        [Header("Survival")]
        public float health;
        public float healthRegen;
        public float armor;
        public float magicResistance;
        public float dodgeChance;
        public float lifesteal;

        [Header("Combat")]
        public float damage;
        public float attackSpeed;
        public float critChance;
        public float critDamage;
        public float bulletSpeed;

        [Header("Movement")]
        public float movementSpeed;
    }

    [Header("Base Stats")]
    public Stats stats;

    [Header("Runtime Stats — effective values, updated during play")]
    public Stats runtimeStats;

    public void InitRuntimeStats()
    {
        runtimeStats.health          = stats.health;
        runtimeStats.healthRegen     = stats.healthRegen;
        runtimeStats.armor           = stats.armor;
        runtimeStats.magicResistance = stats.magicResistance;
        runtimeStats.dodgeChance     = stats.dodgeChance;
        runtimeStats.lifesteal       = stats.lifesteal;
        runtimeStats.damage          = stats.damage;
        runtimeStats.attackSpeed     = stats.attackSpeed;
        runtimeStats.critChance      = stats.critChance;
        runtimeStats.critDamage      = stats.critDamage;
        runtimeStats.bulletSpeed     = stats.bulletSpeed;
        runtimeStats.movementSpeed   = stats.movementSpeed;
    }
}