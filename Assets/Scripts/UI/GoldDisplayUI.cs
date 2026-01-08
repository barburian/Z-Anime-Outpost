using UnityEngine;
using TMPro;

public class GoldDisplayUI : MonoBehaviour
{
    private TextMeshProUGUI _goldText;

    private void Awake()
    {
        _goldText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (Player.Instance != null)
        {
            Player.Instance.OnGoldChanged.AddListener(UpdateGoldText);
            
            UpdateGoldText(0);
        }
    }

    private void UpdateGoldText(float currentGold)
    {
        _goldText.text = $"Gold: {currentGold}";
    }

    private void OnDestroy()
    {
        if (Player.Instance != null)
        {
            Player.Instance.OnGoldChanged.RemoveListener(UpdateGoldText);
        }
    }
}