using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Container : MonoBehaviour
{
    public int id;
    public bool containerIsClose;
    [SerializeField] private int _maxFruitInContainer;
    public int maxFruitInConteiner { get { return _maxFruitInContainer; } }
    [SerializeField] private int _currentFruitInContainer;
    public int currentFruitInConteiner { set { if (value > 0) _currentFruitInContainer = value; } get { return _currentFruitInContainer; } }
    [SerializeField] private List<Transform> _fruitPointsInConteiner;

    private void Awake()
    {
        InitializePointInConteiner();
        _maxFruitInContainer = _fruitPointsInConteiner.Count;
        _currentFruitInContainer = 0;
        containerIsClose = false;
    }

    private void InitializePointInConteiner()
    {
        _fruitPointsInConteiner = new List<Transform>();
        foreach (Transform child in transform.GetChild(0))
        {
            _fruitPointsInConteiner.Add(child);
        }
    }

    public Transform GetEmptyPointInConteiner()
    {
        foreach (Transform point in _fruitPointsInConteiner)
        {
            if (point.childCount == 0)
            {
                return point;
            }
        }
        return null;
    }

    public void PutInConteiner(Transform fruit)
    {
        if (fruit)
        {
            Transform emptyPoint = GetEmptyPointInConteiner();
            if (emptyPoint != null)
            {
                fruit.position = emptyPoint.position;
                fruit.SetParent(emptyPoint);
            }
            else
            {
                Debug.Log("No empty point in the Conteiner!");
            }
        }
        else
        {
            Debug.Log("Fruit not selected!");
        }
        if (IsConteinerFull() && AreElementsHomogeneous()) CloseConteiner();
    }

    private void CloseConteiner()
    {
        containerIsClose = true;
        transform.GetChild(3).gameObject.SetActive(true);
        GameManager.updateCloseContainerCount?.Invoke();
        SetRenderMode();
    }
    private void SetRenderMode()
    {
        Renderer wallMatirial = transform.GetChild(1).GetComponent<Renderer>();
        wallMatirial.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        wallMatirial.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        wallMatirial.material.SetInt("_ZWrite", 1);
        wallMatirial.material.DisableKeyword("_ALPHATEST_ON");
        wallMatirial.material.DisableKeyword("_ALPHABLEND_ON");
        wallMatirial.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        wallMatirial.material.renderQueue = -1;
    }

    public bool IsConteinerFull()
    {
        foreach (Transform point in _fruitPointsInConteiner)
        {
            if (point.childCount == 0)
            {
                return false;
            }
        }

        return true;
    }
    private bool AreElementsHomogeneous()
    {
        ItemData firstFruit = _fruitPointsInConteiner[0].GetChild(0).GetComponent<ItemData>();
        int firstFruitID = firstFruit.id;

        foreach (var item in _fruitPointsInConteiner)
        {
            ItemData fruitData = item.GetChild(0).GetComponent<ItemData>();

            if (fruitData.id != firstFruitID)
            {
                return false;
            }
        }
        return true;
    }


}

