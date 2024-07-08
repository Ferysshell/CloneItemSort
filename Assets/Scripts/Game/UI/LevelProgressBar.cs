using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _nextPoint;
    public static Action<int, int> updateProgressBar;

    private void OnEnable()
    {
        updateProgressBar += UpdateProgressBar;
    }

    private void OnDisable()
    {
        updateProgressBar -= UpdateProgressBar;
    }

    private void UpdateProgressBar(int currentLevel, int maxLevel)
    {
        Transform pointsContainer = _progressBar.transform.GetChild(0);
        for (int i = pointsContainer.childCount - 1; i >= 0; i--)
        {
            int point = maxLevel - 5 + i + 1;
            if ((point) == currentLevel)
            {
                FillBar(i);
            }
            TextMeshProUGUI pointText = pointsContainer.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            pointText.text = (point).ToString();
        }

    }

    private void FillBar(int value)
    {
        Image progressBarImage = _progressBar.GetComponent<Image>();
        if (value > 0)
        {
            progressBarImage.fillAmount = value * 0.25f;
        }
        else progressBarImage.fillAmount = 0.075f;
    }
}
