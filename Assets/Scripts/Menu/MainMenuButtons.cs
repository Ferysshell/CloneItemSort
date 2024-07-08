using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button _play, _policy, _quit;
    [SerializeField] private TextMeshProUGUI _coinWallet;
    [SerializeField] private string _url;
    private void Start()
    {
        _coinWallet.text = PlayerPrefs.GetInt("CoinInWallet", 100).ToString();
        _play.onClick.AddListener(Play);
        _policy.onClick.AddListener(Policy);
        _quit.onClick.AddListener(Quit);
    }
    private void Play()
    {
        SoundManager.instance.TapButton();
        LoadScene.instance.LoadSetScene(2);
    }
    private void Policy()
    {
        SoundManager.instance.TapButton();
        if (!string.IsNullOrEmpty(_url))
        {
            Application.OpenURL(_url);
        }
        else
        {
            Debug.LogWarning("URL is not set.");
        }
    }


    private void Quit()
    {
        SoundManager.instance.TapButton();
        Application.Quit();
    }
}






