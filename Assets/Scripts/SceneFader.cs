using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;
    [SerializeField] private Image img;
    [SerializeField] private AnimationCurve curve;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator FadeIn()
    {
        float time = 1f;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            float alfa = curve.Evaluate(time);
            img.color = new Color(0, 0, 0, alfa);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            float alfa = curve.Evaluate(time);
            img.color = new Color(0, 0, 0, alfa);
            yield return null;
        }
    }


}
