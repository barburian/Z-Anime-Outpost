using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class EnemyConfig
    {
        public EnemyData enemyData;
        public int count; 
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string groupName;
        public List<EnemyConfig> enemiesInGroup;
        public int repeatCount = 5; 
        public float spawnInterval = 2f; 
        public float delayAfterGroup = 1f; 
    }

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public float waveDuration = 60f;
        public List<EnemyGroup> groups;
    }

    [Header("Setări Generale")]
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private List<Wave> waves;

    private int _currentWaveIndex = 0;
    private Transform player;
    private float _currentWaveTime = 0f; 
    private bool _isWaveActive = false;

    void Start()
    {
        if (Player.Instance != null)
        {
            player = Player.Instance.transform;
            StartCoroutine(WaveRoutine());
        }
        else
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
                StartCoroutine(WaveRoutine());
            }
        }
    }

    void Update()
    {
        if (_isWaveActive)
        {
            _currentWaveTime += Time.deltaTime;
        }
    }

    IEnumerator WaveRoutine()
    {
        while (_currentWaveIndex < waves.Count && player != null)
        {
            Wave currentWave = waves[_currentWaveIndex];
            _currentWaveTime = 0f;
            _isWaveActive = true;
            
            foreach (EnemyGroup group in currentWave.groups)
            {
                if (_currentWaveTime >= currentWave.waveDuration) break;

                for (int r = 0; r < group.repeatCount; r++)
                {
                    if (_currentWaveTime >= currentWave.waveDuration) break;
                    if (player == null) yield break;

                    foreach (EnemyConfig config in group.enemiesInGroup)
                    {
                        for (int i = 0; i < config.count; i++)
                        {
                            SpawnEnemy(config.enemyData);
                            yield return new WaitForSeconds(0.1f); 
                        }
                    }

                    float waitTimer = 0f;
                    while (waitTimer < group.spawnInterval)
                    {
                        waitTimer += Time.deltaTime;
                        if (_currentWaveTime >= currentWave.waveDuration) break;
                        yield return null; 
                    }
                }

                float delayTimer = 0f;
                while (delayTimer < group.delayAfterGroup)
                {
                    delayTimer += Time.deltaTime;
                    if (_currentWaveTime >= currentWave.waveDuration) break;
                    yield return null;
                }
            }

            while (_currentWaveTime < currentWave.waveDuration)
            {
                if (player == null) yield break;
                yield return null;
            }

            _isWaveActive = false;
            _currentWaveIndex++;
        }

        StartShop();
    }

    private void StartShop()
    {
        Debug.Log("WAVES FINISHED!");
    }

    private void SpawnEnemy(EnemyData data)
    {
        if (data == null || data.enemyPrefab == null) return;

        Vector2 randomPos = Random.insideUnitCircle.normalized * _spawnRadius;
        Vector2 spawnPos = (Vector2)player.position + randomPos;

        Instantiate(data.enemyPrefab, spawnPos, Quaternion.identity);
    }
}