using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet instance;
    [SerializeField] private int _curretnCoinInWallet;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public int GetNewAmoutOfMoney(int coinCount)
    {
        if (coinCount > 0) _curretnCoinInWallet += coinCount;
        return _curretnCoinInWallet;
    }
}
