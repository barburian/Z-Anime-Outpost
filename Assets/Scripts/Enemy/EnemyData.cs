using UnityEngine;

public enum EnemyCategory
{
    Fast,
    Tank,
    Slow,
    Small,
    Big,
    Magic_Resitant
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Identity")]
    public string enemyName;
    public EnemyCategory enemyCategory;
    public Sprite enemySprite;
    public GameObject enemyPrefab;

    [System.Serializable]
    public class Stats
    {
        public float health;
        public float damage;
        public float enemySpeed; // Movement Speed
        public float attackSpeed;
        public float range;
        public float bulletSpeed;
    }

    [Header("Statistics")]
    public Stats stats;
}