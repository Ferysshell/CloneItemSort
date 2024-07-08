using System;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerTXT;

    private void OnEnable()
    {
        GameManager.OnTimerUpdated += UpdateTimer;
    }

    private void OnDisable()
    {
        GameManager.OnTimerUpdated -= UpdateTimer;
    }

    private void UpdateTimer(float time)
    {
        if (time != 0) _timerTXT.text = time.ToString("F0")+"s";
    }

}
