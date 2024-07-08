using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreenAnimation : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private float switchInterval = 1f;

    private int currentIndex = 0;

    private void Start()
    {
        StartCoroutine(SwitchImages());
    }

    private IEnumerator SwitchImages()
    {
        while (true)
        {
            int previousIndex = (currentIndex == 0) ? images.Length - 1 : currentIndex - 1;
            images[currentIndex].enabled = true;
            images[previousIndex].enabled = false;

            currentIndex = (currentIndex + 1) % images.Length;
            yield return new WaitForSeconds(switchInterval);
        }
    }
}
