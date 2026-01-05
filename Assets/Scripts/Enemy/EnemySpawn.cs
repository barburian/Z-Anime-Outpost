using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{   [SerializeField] private GameObject _enemyPrefab; 
    [SerializeField] private float _spawnInterval = 2f; 
    [SerializeField] private float _spawnRadius = 10f;

    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            // Pornim cronometrul de spawn
            StartCoroutine(SpawnEnemyRoutine());
        }

    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (player != null)
        {
            SpawnEnemy();
            // Așteptăm x secunde înainte să repetăm
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomPos = Random.insideUnitCircle.normalized * _spawnRadius;
        
        Vector2 spawnPos = (Vector2)player.position + randomPos;

        Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {

    }
}
