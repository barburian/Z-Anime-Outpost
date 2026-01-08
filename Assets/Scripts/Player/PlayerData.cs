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

    [Header("Statistics")]
    public Stats stats; // Aici sunt grupate toate variabilele de mai sus
}