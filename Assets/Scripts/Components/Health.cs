using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Setări")]
    private float maxHealth;
    private float currentHealth;

    [Header("Events (Opțional)")]
    public UnityEvent OnDeath;
    public UnityEvent<float> OnDamageTaken;

    [Header("Loot")]
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private GameObject xpGemPrefab;
    private float dropAmount;
    private float xpDropAmount;

    public float MaxHealth => maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void InitializeHealth(float healthValue)
    {
        maxHealth = healthValue;
        currentHealth = healthValue;
    }

    private const float ArmorK = 100f;

    public void TakeDamage(float amount)
    {
        if (gameObject.CompareTag("Player") && Player.Instance != null)
        {
            float armor = Player.Instance.GetEffectiveArmor();
            amount = amount * (ArmorK / (ArmorK + armor));
            amount = Mathf.Max(1f, amount);
        }

        currentHealth -= amount;
        OnDamageTaken?.Invoke(currentHealth / maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void AddMaxHealth(float delta, bool healDelta)
    {
        maxHealth += delta;
        if (healDelta)
            currentHealth = Mathf.Min(currentHealth + delta, maxHealth);
        OnDamageTaken?.Invoke(currentHealth / maxHealth);
    }

    public void SetDropAmount(float amount) { dropAmount = amount; }
    public void SetXPDropAmount(float amount) { xpDropAmount = amount; }

    private void Die()
    {
        OnDeath?.Invoke();

        LootBag lootBag = GetComponent<LootBag>();
        if (lootBag != null)
            lootBag.TryDropLoot();

        if (gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        else
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
    }
}
