using UnityEngine;

public enum AnimationDirection { Down, Up, Right, Left }
public enum AnimationState    { Idle, Moving }

/// <summary>
/// Drives a SpriteRenderer from pre-sliced sprite arrays.
/// Assign idle poses (1 sprite each) and walk frames in the Inspector.
/// Call UpdateAnimation() every frame from a movement script.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private float _frameRate = 8f;

    [Header("Idle Poses — one sprite per direction")]
    [SerializeField] private Sprite _idleDown;
    [SerializeField] private Sprite _idleUp;
    [SerializeField] private Sprite _idleRight;
    [SerializeField] private Sprite _idleLeft;

    [Header("Walk Animations — drag sliced sprites in order")]
    [SerializeField] private Sprite[] _walkDown;
    [SerializeField] private Sprite[] _walkUp;
    [SerializeField] private Sprite[] _walkRight;
    [SerializeField] private Sprite[] _walkLeft;

    private SpriteRenderer     _sr;
    private Sprite[]           _walkFrames;
    private AnimationDirection _dir   = AnimationDirection.Down;
    private AnimationState     _state = AnimationState.Idle;
    private int                _frame;
    private float              _timer;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        Refresh(resetFrame: true);
    }

    // Called every frame by movement scripts.
    public void UpdateAnimation(AnimationDirection dir, AnimationState state)
    {
        if (dir == _dir && state == _state) return;

        bool stateChanged = state != _state;
        _dir   = dir;
        _state = state;
        // Only restart the frame counter when transitioning between Idle and Moving.
        // Direction-only changes (e.g. left -> right) keep the frame counter running
        // so the animation doesn't visibly stutter or flash frame-0.
        Refresh(resetFrame: stateChanged);
    }

    void Update()
    {
        if (_state != AnimationState.Moving) return;
        if (_walkFrames == null || _walkFrames.Length == 0) return;

        _timer += Time.deltaTime;
        if (_timer >= 1f / _frameRate)
        {
            _timer -= 1f / _frameRate;
            _frame  = (_frame + 1) % _walkFrames.Length;
            _sr.sprite = _walkFrames[_frame];
        }
    }

    private void Refresh(bool resetFrame)
    {
        if (_state == AnimationState.Moving)
        {
            Sprite[] next = WalkFramesFor(_dir);

            if (next != _walkFrames)
            {
                _walkFrames = next;
                // Wrap the existing frame index into the new clip's range so there
                // is no hard restart on a direction change.
                if (resetFrame || _walkFrames == null || _walkFrames.Length == 0)
                    _frame = 0;
                else
                    _frame %= _walkFrames.Length;
                _timer = 0f;
            }

            if (_walkFrames != null && _walkFrames.Length > 0)
                _sr.sprite = _walkFrames[_frame];
        }
        else
        {
            _walkFrames = null;
            _frame      = 0;
            _timer      = 0f;
            Sprite idle = IdleSpriteFor(_dir);
            if (idle != null) _sr.sprite = idle;
        }
    }

    private Sprite[] WalkFramesFor(AnimationDirection dir) => dir switch
    {
        AnimationDirection.Down  => _walkDown,
        AnimationDirection.Up    => _walkUp,
        AnimationDirection.Right => _walkRight,
        _                        => _walkLeft,
    };

    private Sprite IdleSpriteFor(AnimationDirection dir) => dir switch
    {
        AnimationDirection.Down  => _idleDown,
        AnimationDirection.Up    => _idleUp,
        AnimationDirection.Right => _idleRight,
        _                        => _idleLeft,
    };
}
