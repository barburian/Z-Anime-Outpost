// FILE: Assets/Scripts/Items/ItemPickup.cs
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Data")]
    public ItemData itemData; // You drag your "Spinach" or "Shield" data here

    [Header("Visuals")]
    [SerializeField] private bool _autoSetSprite = true;
    [SerializeField] private float _bobSpeed = 2f;
    [SerializeField] private float _bobHeight = 0.2f;

    private Vector3 _startPos;
    private SpriteRenderer _sr;

    void Start()
    {
        _startPos = transform.position;
        _sr = GetComponent<SpriteRenderer>();

        // Automatically change the game object's sprite to match the ScriptableObject icon
        if (_autoSetSprite && itemData != null && itemData.icon != null)
        {
            _sr.sprite = itemData.icon;
        }
    }

    void Update()
    {
        // Optional: Make the item float up and down to look "collectible"
        float newY = _startPos.y + Mathf.Sin(Time.time * _bobSpeed) * _bobHeight;
        transform.position = new Vector3(_startPos.x, newY, _startPos.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
       
        
        // Check if the object touching me is the Player
        if (other.CompareTag("Player"))
        {  
            // Apply the stats
            Debug.Log(Player.Instance);

            if (Player.Instance != null && Player.Instance.statsManager != null)
            {   Debug.Log($"Applying Item: ");
                Player.Instance.statsManager.AddItem(itemData);
                 Debug.Log("am intrat in AddItem");
                // Optional: Play a sound effect here
                // AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                // Destroy the physical item from the world
                Destroy(gameObject);
            }
        }
    }
}