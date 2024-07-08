using UnityEngine;

public class FPSManager : MonoBehaviour
{
    private static FPSManager _instance;
    [SerializeField] private int _fpsTargert;
    private bool _isTargetFrameRateSet = false;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SetTargetFrameRate();
        }
    }

    private void SetTargetFrameRate()
    {
        if (!_isTargetFrameRateSet)
        {
            Application.targetFrameRate = _fpsTargert;
            _isTargetFrameRateSet = true;
        }
    }
}