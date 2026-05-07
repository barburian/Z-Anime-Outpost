using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Setări Generale")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Setări Vizuale")]
    [SerializeField] private GameObject weaponVisualPrefab;
    [SerializeField] private float weaponOrbitRadius = 1.5f;

    [SerializeField] private List<WeaponData> startingWeapons;

    private List<ActiveWeapon> myWeapons = new List<ActiveWeapon>();

    void Start()
    {
        foreach (WeaponData data in startingWeapons)
        {
            AddWeapon(data);
        }
    }

    void FixedUpdate()
    {

        foreach (ActiveWeapon weapon in myWeapons)
        {
            Vector3 searchOrigin = transform.position;
            if (weapon.visualObj != null)
            {
                searchOrigin = weapon.visualObj.transform.position;
            }

            Transform individualTarget = FindClosestEnemy(searchOrigin);

            if (individualTarget == null) continue;

            if (weapon.visualObj != null)
            {
                Vector2 dir = individualTarget.position - weapon.visualObj.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                weapon.visualObj.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (Time.time >= weapon.nextFireTime)
            {
                Fire(weapon, individualTarget);
                weapon.nextFireTime = Time.time + weapon.data.stats.fireRate;
            }
        }
    }

    public void AddWeapon(WeaponData data)
    {
        GameObject newVisual = null;

        if (weaponVisualPrefab == null && data.weaponSprite == null)
        {
         
        }
        else
        {

            newVisual = Instantiate(weaponVisualPrefab, transform);
            
            var sr = newVisual.GetComponentInChildren<SpriteRenderer>();
            
            if (sr != null)sr.sprite = data.weaponSprite;
            
        
        }

        ActiveWeapon newWeapon = new ActiveWeapon
        {
            data = data,
            visualObj = newVisual,
            nextFireTime = 0f
        };

        myWeapons.Add(newWeapon);
        ArrangeWeaponsInCircle();
    }

    void Fire(ActiveWeapon weapon, Transform target)
    {
        if (weapon.data.bulletPrefab == null) return;


        Vector3 spawnPos = transform.position;
        if (weapon.visualObj != null) 
        {
            spawnPos = weapon.visualObj.transform.position;
        }

        Vector2 direction = (target.position - spawnPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject bulletObj = ObjectPool.Instance.Get(weapon.data.bulletPrefab, spawnPos, rotation);

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Setup(weapon.data.stats.damage, weapon.data.stats.bulletSpeed);
        }
    }

    void ArrangeWeaponsInCircle()
    {
        int count = myWeapons.Count;
        if (count == 0) return;

        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            if (myWeapons[i].visualObj != null)
            {
                float currentAngle = i * angleStep;
                float radian = currentAngle * Mathf.Deg2Rad;

                float x = Mathf.Cos(radian) * weaponOrbitRadius;
                float y = Mathf.Sin(radian) * weaponOrbitRadius;

                myWeapons[i].visualObj.transform.localPosition = new Vector3(x, y, 0);
            }
        }
    }


    Transform FindClosestEnemy(Vector3 sourcePosition)
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(sourcePosition, detectionRadius, enemyLayer);
        
        Transform bestTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            // Calculăm distanța față de sursă (armă)
            float dist = Vector2.Distance(sourcePosition, enemy.transform.position);
            
            if (dist < minDistance)
            {
                minDistance = dist;
                bestTarget = enemy.transform;
            }
        }
        return bestTarget;
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

[System.Serializable]
public class ActiveWeapon
{
    public WeaponData data;
    public GameObject visualObj;
    public float nextFireTime;
}