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
    public string enemyName;
    public  EnemyCategory enemyCategory;
    public  Sprite enemySprite;
    public  GameObject enemyPrefab; 
    public  float attackspeed; 
    public  float damage;
    public  float range;
    public  float bulletSpeed; 
}
