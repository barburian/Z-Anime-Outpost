using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mapRenderer;
    [SerializeField] private PlayerData     _playerData;

    private float              _moveSpeed;
    private float              _minX, _maxX, _minY, _maxY;
    private Vector2            _moveInput;
    private Rigidbody2D        _rb;
    private SpriteAnimator     _animator;
    private AnimationDirection _lastDir = AnimationDirection.Down;

    void Start()
    {
        _rb        = GetComponent<Rigidbody2D>();
        _animator  = GetComponent<SpriteAnimator>();
        _moveSpeed = _playerData.stats.movementSpeed;

        float pw = GetComponent<SpriteRenderer>().bounds.extents.x;
        float ph = GetComponent<SpriteRenderer>().bounds.extents.y;

        _minX = _mapRenderer.bounds.min.x + pw;
        _maxX = _mapRenderer.bounds.max.x - pw;
        _minY = _mapRenderer.bounds.min.y + ph;
        _maxY = _mapRenderer.bounds.max.y - ph;
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        bool moving = _moveInput.sqrMagnitude > 0.01f;

        if (moving)
            _lastDir = DirectionFrom(_moveInput, _lastDir);

        _animator?.UpdateAnimation(_lastDir, moving ? AnimationState.Moving : AnimationState.Idle);
    }

    void FixedUpdate()
    {
        Vector2 next = _rb.position + _moveInput * _moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(new Vector2(
            Mathf.Clamp(next.x, _minX, _maxX),
            Mathf.Clamp(next.y, _minY, _maxY)
        ));
    }

    // Picks the dominant axis. Requires one axis to be at least 20% larger than
    // the other before committing to a new direction, preventing flicker when
    // a joystick sits near 45 degrees.
    private static AnimationDirection DirectionFrom(Vector2 input, AnimationDirection current)
    {
        float ax = Mathf.Abs(input.x);
        float ay = Mathf.Abs(input.y);

        if (ax > ay * 1.2f)
            return input.x > 0 ? AnimationDirection.Right : AnimationDirection.Left;
        if (ay > ax * 1.2f)
            return input.y > 0 ? AnimationDirection.Up : AnimationDirection.Down;

        return current; // ambiguous diagonal — keep last committed direction
    }
}
