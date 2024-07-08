using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _menus;
    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    public void ActivateMenu(int id)
    {
        _menus[id].SetActive(true);
    }

}
