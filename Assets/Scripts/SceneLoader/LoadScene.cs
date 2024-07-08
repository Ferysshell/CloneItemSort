using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;
    private static bool _firstLoad = false;
    [SerializeField] private float _minInterval;
    [SerializeField] private float _maxInterval;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        if (!_firstLoad) FirstLoadScene();
    }

    public void LoadSetScene(int sceneIndex)
    {
        SceneManager.LoadScene(0);
        StartCoroutine(LoadSceneAtRandomIntervals(sceneIndex));
    }

    private void FirstLoadScene()
    {
        StartCoroutine(LoadSceneAtRandomIntervals(1));
        _firstLoad = true;
    }
    private IEnumerator LoadSceneAtRandomIntervals(int sceneIndex)
    {
        StartCoroutine(SceneFader.instance.FadeIn());
        float waitTime = Random.Range(_minInterval, _maxInterval);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(SceneFader.instance.FadeOut());
        yield return new WaitForSeconds(0.9f);
        StartCoroutine(SceneFader.instance.FadeIn());
        SceneManager.LoadScene(sceneIndex);
    }
}
