using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Container info")]
    [SerializeField] private int _countSpawnedContainer;
    [SerializeField] private int _countClosedContainer;
    [SerializeField] private ContainerSpawner _containerSpawner;
    [Header("Game Info")]
    [SerializeField] private bool _gameOver;
    public float _timer = 0;
    public int currentLevel;
    public int maxLevel;
    public int coinInWallet;
    [SerializeField] private int _reward;
    public static Action updateCloseContainerCount;
    public static Action<float> OnTimerUpdated;
    private void OnEnable()
    {
        updateCloseContainerCount += UpdateCloseContainerCount;
    }
    private void OnDisable()
    {
        updateCloseContainerCount -= UpdateCloseContainerCount;
    }
    private void Start()
    {
        _gameOver = false;
        SpawnContainerAndUpdateCount();
        StartCoroutine(StartTimer());
        SaveDataManager.instance.LoadData(ref currentLevel, ref maxLevel, ref coinInWallet);
        TopMenu.updateCoinWallet?.Invoke(coinInWallet);
        if (currentLevel == 1 || currentLevel > maxLevel) maxLevel += 5;
        LevelProgressBar.updateProgressBar?.Invoke(currentLevel, maxLevel);
    }
    private void SpawnContainerAndUpdateCount()
    {
        if (_containerSpawner)
        {
            _countSpawnedContainer = _containerSpawner.SpawnContainer();

        }
    }
    public IEnumerator StartTimer()
    {
        while (!_gameOver)
        {
            _timer += Time.deltaTime;
            OnTimerUpdated?.Invoke(_timer);
            yield return null;
        }
    }
    private void GameOver()
    {
        _gameOver = true;
        StopCoroutine(StartTimer());
        UIManager.instance.ActivateMenu(0);
        GameOverMenu.setEndScore?.Invoke(_timer);
        currentLevel++;
        int currentCoinInWallet = Wallet.instance.GetNewAmoutOfMoney(coinInWallet + _reward);
        GameOverMenu.updateCoinCounter?.Invoke(currentCoinInWallet);
        SaveDataManager.instance.SaveData(currentLevel, maxLevel, currentCoinInWallet);
    }
    private void UpdateCloseContainerCount()
    {
        _countClosedContainer++;
        if (_countClosedContainer == _countSpawnedContainer - 1) GameOver();
    }
}
