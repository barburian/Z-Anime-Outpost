using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{   [SerializeField] private GameObject _enemyPrefab; 
    [SerializeField] private float _spawnInterval = 2f; 
    [SerializeField] private float _spawnRadius = 10f;
    private WaitForSeconds _spawnWait;

    private Transform player;

    void Start()
    {
        _spawnWait = new WaitForSeconds(_spawnInterval);
        
        if (Player.Instance != null)
        {
            player = Player.Instance.transform;
       
            StartCoroutine(SpawnEnemyRoutine());
        }

    }
    IEnumerator SpawnEnemyRoutine()
{
    var wait = new WaitForSeconds(_spawnInterval);
    while (true)
    {
        if (Player.Instance != null) SpawnEnemy();
        yield return wait;
    }
}

    private void SpawnEnemy()
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
