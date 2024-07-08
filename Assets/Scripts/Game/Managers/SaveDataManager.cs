using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    public void SaveData(int currentLevel, int maxLevel, int coinInWallet)
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("CoinInWallet", coinInWallet);
        PlayerPrefs.SetInt("MaxLevel", maxLevel);
    }
    public void LoadData(ref int currentLevel, ref int maxLevel, ref int coinInWallet)
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        maxLevel = PlayerPrefs.GetInt("MaxLevel");
        coinInWallet = PlayerPrefs.GetInt("CoinInWallet",100);
    }
}
