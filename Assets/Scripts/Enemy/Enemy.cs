using UnityEngine;

public class Enemy : MonoBehaviour
{   public static Enemy Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Awake() 
    {
    // Standard Singleton Pattern: ensure only one player exists
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
