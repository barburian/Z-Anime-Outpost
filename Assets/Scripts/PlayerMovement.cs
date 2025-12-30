using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]private SpriteRenderer mapRenderer;
    public float movSpeed;
    private float _minX, _maxX, _minY, _maxY;
    private Vector2 moveInput;
    
    Rigidbody2D rb;
    void Start()
    {   
        float playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        rb = GetComponent<Rigidbody2D>();
        _minX = mapRenderer.bounds.min.x;
        _maxX = mapRenderer.bounds.max.x;
        _minY = mapRenderer.bounds.min.y;
        _maxY = mapRenderer.bounds.max.y;
        
        _minX += playerWidth; _maxX -= playerWidth;
        _minY += playerWidth; _maxY -= playerWidth;
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
      
    }
     void FixedUpdate()
    {
        Vector2 nextPosition = rb.position + moveInput * movSpeed * Time.fixedDeltaTime;

        float clampedX = Mathf.Clamp(nextPosition.x, _minX, _maxX);
        float clampedY = Mathf.Clamp(nextPosition.y, _minY, _maxY);

        rb.MovePosition(new Vector2(clampedX, clampedY));
    
    }
}
