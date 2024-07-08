using System.Collections.Generic;
using UnityEngine;

public class ContainerSpawner : MonoBehaviour
{
    public static ContainerSpawner instance;
    [SerializeField] private GameObject _containerPrefab;
    [SerializeField] private int _countContainers;
    [SerializeField] private List<GameObject> _containerList = new List<GameObject>();
    public List<GameObject> jarList { get { return _containerList; } }
    private int RandomJarCount()
    {
        return Random.Range(3, 6);
    }
    public int SpawnContainer()
    {
        _countContainers = RandomJarCount();

        int totalSpawnPoints = transform.childCount;

        for (int i = 0; i < _countContainers; i++)
        {
            int spawnIndex = totalSpawnPoints - 1 - i;

            if (spawnIndex >= 0 && _containerList.Count < _countContainers)
            {
                GameObject spawnedObject = Instantiate(_containerPrefab, transform.GetChild(spawnIndex).position, _containerPrefab.transform.rotation);
                spawnedObject.GetComponent<Container>().id = i;
                spawnedObject.name += i;
                spawnedObject.transform.localScale = _containerPrefab.transform.localScale;
                _containerList.Add(spawnedObject);
                spawnedObject.transform.SetParent(transform.GetChild(spawnIndex));
            }
            else
            {
                Debug.LogWarning("Not enough spawn points for jars");
                break;
            }
        }
        FillerSpawner.instance.StartFruitSpawner(_containerList);
        return _containerList.Count;

    }
}
