using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   [SerializeField] private EnemyData enemy;
    private Transform _player;
    private Rigidbody2D _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        
        if (Player.Instance != null)
        {
            _player = Player.Instance.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (_player == null) return;

        Vector2 direction = (_player.position - transform.position).normalized;

        Vector2 newPos = _rb.position + direction * enemy.stats.enemySpeed * Time.fixedDeltaTime;
        
        _rb.MovePosition(newPos);
    }
}
