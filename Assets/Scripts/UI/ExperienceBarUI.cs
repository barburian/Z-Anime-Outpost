using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBarUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        if (Player.Instance != null)
        {
            Player.Instance.OnXPChanged.AddListener(UpdateXPBar);
            Player.Instance.OnLevelUp.AddListener(UpdateLevelText);
            
            UpdateLevelText(Player.Instance.currentLevel);
            UpdateXPBar(Player.Instance.currentXP, Player.Instance.xpToLevelUp);
        }
    }

    private void UpdateXPBar(float current, float max)
    {
        if (xpSlider != null)
        {
            xpSlider.maxValue = max;
            xpSlider.value = current;
        }
    }

    private void UpdateLevelText(int level)
    {
        if (levelText != null)
        {
            levelText.text = $"LVL {level}";
        }
    }
}
