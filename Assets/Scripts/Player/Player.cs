using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private float totalGold = 0;

    [Header("Player Stats")]
    public float playerMaxHealth = 100f; // Valoarea setată în Inspectorul Player-ului

    [Header("Events")]
    public UnityEvent<float> OnGoldChanged;

    void Awake()
    {
        Instance = this;
    }

    public void AddGold(float amount)
    {
        totalGold += amount;
        Debug.Log($"Gold Collected! Total: {totalGold}");
        OnGoldChanged?.Invoke(totalGold);
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