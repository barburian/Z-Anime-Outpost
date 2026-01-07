using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    private Transform _player;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        
        if (Player.Instance != null)
        {
            _player = Player.Instance.transform;
        }
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (_player == null) return;

        Vector2 direction = (_player.position - transform.position).normalized;

        Vector2 newPos = _rb.position + direction * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPos);
    }
}
