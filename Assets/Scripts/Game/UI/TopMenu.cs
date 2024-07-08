using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopMenu : MonoBehaviour
{
    [SerializeField] private Button _restart, _mainMenu;

    [SerializeField] private TextMeshProUGUI _coinWallet;
    public static Action<int> updateCoinWallet;
    private void OnEnable()
    {
        updateCoinWallet += UpdateCoinWallet;
    }
    private void OnDisable()
    {
        updateCoinWallet -= UpdateCoinWallet;
    }

    private void UpdateCoinWallet(int coinCount)
    {
        if (coinCount > 0) _coinWallet.text = coinCount.ToString();
    }

    void Start()
    {
        _restart.onClick.AddListener(Restart);
        _mainMenu.onClick.AddListener(GoMenu);
    }

    private void GoMenu()
    {
        Banner.instance.HideBannerAd();
        SoundManager.instance.TapButton();
        LoadScene.instance.LoadSetScene(1);
    }

    private void Restart()
    {
        Banner.instance.HideBannerAd();
        SoundManager.instance.TapButton();
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        LoadScene.instance.LoadSetScene(currentScene);
    }
}
