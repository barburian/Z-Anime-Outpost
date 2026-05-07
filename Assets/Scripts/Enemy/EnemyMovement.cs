using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyData enemy;

    private Transform          _player;
    private Rigidbody2D        _rb;
    private SpriteAnimator     _animator;
    private AnimationDirection _lastDir = AnimationDirection.Down;

    void Start()
    {
        _rb       = GetComponent<Rigidbody2D>();
        _animator = GetComponent<SpriteAnimator>();

        if (Player.Instance != null)
            _player = Player.Instance.transform;
    }

    void FixedUpdate()
    {
        if (_player == null) return;

        Vector2 direction = ((Vector2)_player.position - _rb.position).normalized;
        bool    moving    = direction.sqrMagnitude > 0.01f;

        if (moving)
            _lastDir = DirectionFrom(direction, _lastDir);

        _animator?.UpdateAnimation(_lastDir, moving ? AnimationState.Moving : AnimationState.Idle);

        _rb.MovePosition(_rb.position + direction * enemy.stats.enemySpeed * Time.fixedDeltaTime);
    }

    // Same 20% dominance rule as the player to prevent direction flicker.
    private static AnimationDirection DirectionFrom(Vector2 dir, AnimationDirection current)
    {
        float ax = Mathf.Abs(dir.x);
        float ay = Mathf.Abs(dir.y);

        if (ax > ay * 1.2f)
            return dir.x > 0 ? AnimationDirection.Right : AnimationDirection.Left;
        if (ay > ax * 1.2f)
            return dir.y > 0 ? AnimationDirection.Up : AnimationDirection.Down;

        return current;
    }
}
