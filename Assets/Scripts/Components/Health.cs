using UnityEngine;
using UnityEngine.Events; // Necesar pentru UI (Lifebar) pe viitor

public class Health : MonoBehaviour, IDamageable
{
    [Header("Setări")]
    private float maxHealth ;
    private float currentHealth;

    [Header("Events (Opțional)")]
    public UnityEvent OnDeath; // Putem lega sunete sau efecte aici din Inspector
    public UnityEvent<float> OnDamageTaken; // Putem lega bara de viață aici



    void Start()
    {
        currentHealth = maxHealth;
    }
    public void InitializeHealth(float healthValue)
    {
        maxHealth = healthValue;
        currentHealth = healthValue;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} a primit {amount} damage. Viață rămasă: {currentHealth}");

        // Invocăm evenimentul (pentru bara de viață, flash roșu, etc.)
        OnDamageTaken?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [Header("Loot")]
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private GameObject xpGemPrefab;
    private float dropAmount;
    private float xpDropAmount;

    public void SetDropAmount(float amount) { dropAmount = amount; }
    public void SetXPDropAmount(float amount) { xpDropAmount = amount; }

    private void Die()
    {
        OnDeath?.Invoke();
        
        if (!gameObject.CompareTag("Player"))
        {
            if (goldPrefab != null)
            {
                GameObject goldObj = ObjectPool.Instance.Get(goldPrefab, transform.position, Quaternion.identity);
                goldObj.GetComponent<Gold>().SetValue(dropAmount);
            }
            if (xpGemPrefab != null)
            {
                GameObject gemObj = ObjectPool.Instance.Get(xpGemPrefab, transform.position, Quaternion.identity);
                gemObj.GetComponent<XPGem>().SetValue(xpDropAmount);
            }
            ObjectPool.Instance.Return(gameObject);
        }
        else
        {   
            gameObject.SetActive(false);
        }
        if(gameObject.CompareTag("Enemy"))
        {}
        LootBag lootBag = GetComponent<LootBag>();
            if (lootBag != null)
            {  
                lootBag.TryDropLoot();
            }
        gameObject.SetActive(false);
    }
}