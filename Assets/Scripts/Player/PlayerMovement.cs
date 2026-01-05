using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]private SpriteRenderer _mapRenderer;
    [SerializeField]private float _moveSpeed;
    private float _minX, _maxX, _minY, _maxY;
    private Vector2 _moveInput;
    
    Rigidbody2D rb;
    void Start()
    {   
        float playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float playerHeight = GetComponent<SpriteRenderer>().bounds.extents.y;


        rb = GetComponent<Rigidbody2D>();
        _minX = _mapRenderer.bounds.min.x;
        _maxX = _mapRenderer.bounds.max.x;
        _minY = _mapRenderer.bounds.min.y;
        _maxY = _mapRenderer.bounds.max.y;
        
        _minX += playerWidth; _maxX -= playerWidth;
        _minY += playerHeight; _maxY -= playerHeight;
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
      
    }
     void FixedUpdate()
    {
        Vector2 nextPosition = rb.position + _moveInput * _moveSpeed * Time.fixedDeltaTime;

        float clampedX = Mathf.Clamp(nextPosition.x, _minX, _maxX);
        float clampedY = Mathf.Clamp(nextPosition.y, _minY, _maxY);

        rb.MovePosition(new Vector2(clampedX, clampedY));
    
    }
}
