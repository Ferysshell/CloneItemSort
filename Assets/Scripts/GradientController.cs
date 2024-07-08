using UnityEngine;
using UnityEngine.UI;

public class GradientController : MonoBehaviour
{
    private Image _image;
    [SerializeField] private Color _color1 = Color.red;
    [SerializeField] private Color _color2 = Color.blue;
    [SerializeField] private float duration = 2.0f;
    [SerializeField] private float speed = 1.0f;
    private float _timeOffset = 0f;

    void Start()
    {
        _image = GetComponent<Image>();

        _timeOffset = Time.time;
    }

    void Update()
    {
        float t = Mathf.PingPong((Time.time - _timeOffset) * speed / duration, 1f);
        Color lerpedColor = Color.Lerp(_color1, _color2, t);
        _image.color = lerpedColor;
    }
}