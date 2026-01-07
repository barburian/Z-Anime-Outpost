using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public EnemyData enemyData; 
        public int count;
        public float spawnInterval;
    }

    [Header("Setări Generale")]
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private float _timeBetweenWaves = 3f;

    [Header("Configurare Valuri")]
    [SerializeField] private List<Wave> waves; 

    private int _currentWaveIndex = 0;
    private Transform player;

    void Start()
    {
        // Găsim player-ul (Singleton sau Tag)
        if (Player.Instance != null)
        {
            player = Player.Instance.transform;
            StartCoroutine(WaveRoutine());
        }
        else
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.transform;
                StartCoroutine(WaveRoutine());
            }
        }
    }

    IEnumerator WaveRoutine()
    {
        while (_currentWaveIndex < waves.Count && player != null)
        {
            Wave currentWave = waves[_currentWaveIndex];


            if (currentWave.enemyData == null)
            {
                Debug.LogError($"Eroare: Valul {_currentWaveIndex} nu are EnemyData setat!");
                yield break;
            }

            for (int i = 0; i < currentWave.count; i++)
            {
                if (player == null) yield break;


                SpawnEnemy(currentWave.enemyData);

                yield return new WaitForSeconds(currentWave.spawnInterval);
            }

            yield return new WaitForSeconds(_timeBetweenWaves);
            _currentWaveIndex++;
        }
    }

    private void SpawnEnemy(EnemyData data)
    {

        Vector2 randomPos = Random.insideUnitCircle.normalized * _spawnRadius;
        Vector2 spawnPos = (Vector2)player.position + randomPos;

      
        if (data.enemyPrefab != null)
        {
            GameObject newEnemy = Instantiate(data.enemyPrefab, spawnPos, Quaternion.identity);

            
        }
        else
        {
            Debug.LogError($"Eroare: EnemyData '{data.name}' nu are un Prefab atribuit!");
        }
    }
}