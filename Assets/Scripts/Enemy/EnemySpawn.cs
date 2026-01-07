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

    // 3. Valul Principal (Acum are o DURATĂ MAXIMĂ)
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        
        [Tooltip("Durata totală a valului (în secunde). Când expiră, jocul se termină sau trecem mai departe.")]
        public float waveDuration = 60f; // <--- waveTimer-ul cerut de tine
        
        public List<EnemyGroup> groups;
    }

    [Header("Setări Generale")]
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private List<Wave> waves;

    private int _currentWaveIndex = 0;
    private Transform player;
    
    // Variabile pentru timer
    private float _currentWaveTime = 0f; // Timer-ul intern care numără
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

    // Folosim Update doar pentru a număra timpul (util dacă vrei să afișezi ceasul pe ecran)
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
            
            // Resetăm timer-ul pentru noul val
            _currentWaveTime = 0f;
            _isWaveActive = true;
            
            Debug.Log($"--- START VAL: {currentWave.waveName} (Durată: {currentWave.waveDuration}s) ---");

            // Iterăm prin grupuri
            foreach (EnemyGroup group in currentWave.groups)
            {
                // VERIFICARE TIMP: Dacă timpul a expirat deja, ieșim din bucla de grupuri
                if (_currentWaveTime >= currentWave.waveDuration) break;

                for (int r = 0; r < group.repeatCount; r++)
                {
                    // VERIFICARE TIMP: Verificăm înainte de fiecare spawn
                    if (_currentWaveTime >= currentWave.waveDuration) break;

                    if (player == null) yield break;

                    // Spawnăm inamicii
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
                        
                        // Dacă în timp ce așteptăm intervalul, timpul total expiră -> STOP
                        if (_currentWaveTime >= currentWave.waveDuration) break;
                        
                        yield return null; // Așteaptă un frame
                    }
                }

                // Pauza după grup (cu aceeași logică de verificare a timpului)
                float delayTimer = 0f;
                while (delayTimer < group.delayAfterGroup)
                {
                    delayTimer += Time.deltaTime;
                    if (_currentWaveTime >= currentWave.waveDuration) break;
                    yield return null;
                }
            }

            // Aici ajungem în 2 cazuri:
            // 1. S-au terminat grupurile (dar mai e timp).
            // 2. A expirat timpul (waveDuration).

            // Cazul 1: Dacă s-au terminat monștrii, dar timpul nu a trecut, așteptăm să treacă timpul
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
        Debug.Log("JOC TERMINAT! AI SUPRAVIEȚUIT!");
        // Aici poți opri timpul: Time.timeScale = 0;
        // Sau poți afișa ecranul de victorie: UIManager.Instance.ShowWinScreen();
    }

    private void SpawnEnemy(EnemyData data)
    {
        if (data == null || data.enemyPrefab == null) return;

        Vector2 randomPos = Random.insideUnitCircle.normalized * _spawnRadius;
        Vector2 spawnPos = (Vector2)player.position + randomPos;

        Instantiate(data.enemyPrefab, spawnPos, Quaternion.identity);
    }
}