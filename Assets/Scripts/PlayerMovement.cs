using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public SpriteRenderer mapRenderer;
    public float movSpeed;
    private float minX, maxX, minY, maxY;
    private Vector2 moveInput;
    
    Rigidbody2D rb;
    void Start()
    {   
        float playerWidth = transform.localScale.x / 2;

        
        rb = GetComponent<Rigidbody2D>();
        minX = mapRenderer.bounds.min.x;
        maxX = mapRenderer.bounds.max.x;
        minY = mapRenderer.bounds.min.y;
        maxY = mapRenderer.bounds.max.y;
        
        minX += playerWidth; maxX -= playerWidth;
        minY += playerWidth; maxY -= playerWidth;
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Valoare Input: " + moveInput); 
    }
    // Update is called once per frame
    void Update()
    {
  Vector2 nextPosition = rb.position + moveInput * movSpeed * Time.fixedDeltaTime;

        // Aici aplicam limitele calculate automat
        float clampedX = Mathf.Clamp(nextPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(nextPosition.y, minY, maxY);

        rb.MovePosition(new Vector2(clampedX, clampedY));
    
    }
}
