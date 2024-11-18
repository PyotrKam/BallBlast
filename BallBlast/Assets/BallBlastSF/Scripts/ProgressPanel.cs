using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressPanel : MonoBehaviour
{
    [Header("UI Elements")]
    public Image progressBar;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    private int totalStones;
    private int destroyedStones;

    private void Start()
    {
        UpdateLevelTexts();
    }

    public void InitializeProgressBar(int totalStones)
    {
        this.totalStones = totalStones;
        destroyedStones = 0;
        UpdateProgressBar();
    }

    public void StoneDestroyed()
    {
        destroyedStones++;
        UpdateProgressBar();
    }

    public void AddToTotalStones(int additionalStones)
    {
        totalStones += additionalStones;
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        if (totalStones > 0)
        {
            progressBar.fillAmount = (float)destroyedStones / totalStones;
        }
        else
        {
            progressBar.fillAmount = 1f;
        }
    }

    public void UpdateLevelTexts()
    {
        currentLevelText.text = LevelManager.currentLevel.ToString();
        nextLevelText.text = (LevelManager.currentLevel + 1).ToString();
    }
}
