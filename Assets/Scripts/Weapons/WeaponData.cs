using System.Collections.Generic;
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
    public string weaponName;
    public  WeaponCategory weaponType;
    public  Sprite weaponSprite;
    public  GameObject bulletPrefab; 
    public  float fireRate; 
    public  float damage;
    public  float range;
    public  float bulletSpeed;
    
}
