using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTXT, _timeTXT, _coinCounter;
    [SerializeField] private Button _nextLevel;
    [SerializeField] private int _maxCoefficient;
    [SerializeField] private int _currentCoefficient;
    public static Action<float> setEndScore;
    public static Action<int> updateCoinCounter;

    private void OnEnable()
    {
        setEndScore += CalculateScore;
        updateCoinCounter += UpdateCoinCounter;
    }
    private void OnDisable()
    {
        setEndScore -= CalculateScore;
        updateCoinCounter -= UpdateCoinCounter;
    }

    private void UpdateCoinCounter(int coin)
    {
        if (coin > 0) _coinCounter.text = coin.ToString();
    }

    private void Start()
    {
        Banner.instance.HideBannerAd();
        _nextLevel.onClick.AddListener(ClickNextLevelBTN);
    }

    private void ClickNextLevelBTN()
    {
        SoundManager.instance.TapButton();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        LoadScene.instance.LoadSetScene(currentScene);
    }

    private void CalculateScore(float time)
    {
        if (time > 0)
        {
            int scorePerSecond = SetCoefficient(time);
            _currentCoefficient = Math.Clamp(_maxCoefficient - scorePerSecond, 1, _maxCoefficient);
            float score = time * _currentCoefficient;
            _timeTXT.text = time.ToString("F2");
            _scoreTXT.text = score.ToString("F0");
        }
    }
    private int SetCoefficient(float time)
    {
        return (int)time / 3;
    }

}
