using UnityEngine;

public class XPGem : MonoBehaviour, IPoolable
{
    private float _value;

    public void SetValue(float value) => _value = value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.AddXP(_value);
            ObjectPool.Instance.Return(gameObject);
        }
    }

    public void OnSpawn() { }
    public void OnDespawn() => _value = 0f;
}
