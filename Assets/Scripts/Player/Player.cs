using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [Header("Player Stats")]
    public float playerMaxHealth = 100f; // Valoarea setată în Inspectorul Player-ului

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Player-ul își inițializează singur componenta Health
        Health myHealth = GetComponent<Health>();
        if (myHealth != null)
        {
            myHealth.InitializeHealth(playerMaxHealth);
        }
    }
}