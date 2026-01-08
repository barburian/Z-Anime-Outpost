using UnityEngine;

public enum WeaponCategory
{
    Pistol,
    Shotgun,
    AssaultRifle,
    Sniper,
    Melee
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Identity")]
    public string weaponName;
    public WeaponCategory weaponType;
    public Sprite weaponSprite;
    public GameObject bulletPrefab;

    [System.Serializable]
    public class Stats
    {
        public float damage;
        public float fireRate;
        public float range;
        public float bulletSpeed;
    }

    [Header("Statistics")]
    public Stats stats;
}