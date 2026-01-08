using UnityEngine;

public class Gold : MonoBehaviour
{
    private float _value;

    public void SetValue(float value)
    {
        _value = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.AddGold(_value);
            Destroy(gameObject);
        }
    }
}