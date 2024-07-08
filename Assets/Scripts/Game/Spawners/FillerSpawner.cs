using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FillerSpawner : MonoBehaviour
{
    public static FillerSpawner instance;
    [SerializeField] private List<Transform> _fillersList;
    private Dictionary<Transform, int> _fillerCountDict;
    [SerializeField] private List<Transform> _setFillerList;
    [SerializeField] private int _lastFillerId = -1; 
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        _fillerCountDict = new Dictionary<Transform, int>();
    }

    public void StartFruitSpawner(List<GameObject> containerList)
    {
        SetRandomFruitForSpawn(containerList.Count);
        FruitSpawn(containerList);
    }

    private void SetRandomFruitForSpawn(int countcontainer)
    {
        int totalFruitsToSpawn = countcontainer - 1;
        int fruitsSpawned = 0;

        while (fruitsSpawned < totalFruitsToSpawn)
        {
            int fruitIndex = Random.Range(0, _fillersList.Count);
            Transform fruit = _fillersList[fruitIndex];

            if (!_fillerCountDict.ContainsKey(fruit))
            {
                _setFillerList.Add(fruit);
                _fillerCountDict[fruit] = 0;
                fruitsSpawned++;
            }
        }
    }

    private void FruitSpawn(List<GameObject> containerList)
    {
        foreach (GameObject containerObject in containerList)
        {
            Container container = containerObject.GetComponent<Container>();
            while (container.currentFruitInConteiner < container.maxFruitInConteiner - 1)
            {
                if (_fillerCountDict.Count == 0)
                {
                    break;
                }

                int randomFruitIndex;
                do
                {
                    randomFruitIndex = Random.Range(0, _fillerCountDict.Count);
                } while (randomFruitIndex == _lastFillerId && _fillerCountDict.Count > 1);

                Transform randomFruit = _fillerCountDict.Keys.ElementAt(randomFruitIndex);
                if (_fillerCountDict[randomFruit] < container.maxFruitInConteiner)
                {
                    Transform newFruit = Instantiate(randomFruit);
                    newFruit.localScale = new Vector3(3f, 2f, 3f);
                    container.PutInConteiner(newFruit);
                    container.currentFruitInConteiner++;

                    _fillerCountDict[randomFruit]++;
                    if (_fillerCountDict[randomFruit] >= container.maxFruitInConteiner)
                    {
                        _fillerCountDict.Remove(randomFruit);
                    }
                    
                    _lastFillerId = randomFruitIndex;
                }
            }
        }
    }
}