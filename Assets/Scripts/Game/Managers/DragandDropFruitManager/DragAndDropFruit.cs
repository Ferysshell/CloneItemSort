using System.Collections;
using UnityEngine;

public class DragAndDropFruit : MonoBehaviour
{
    [SerializeField] private Container _newJar;
    [SerializeField] private AudioClip _putInContainerSound;
    [Header("FruitSetting")]
    [SerializeField] private bool _fruitMove;
    public bool fruitMove { get { return _fruitMove; } }
    [SerializeField] private float _moveSpeed;
    private void Start()
    {
        _fruitMove = false;
    }
    public void SetFruit(Transform fruit, GameObject newJar, Transform startPoint)
    {
        if (fruit && newJar && startPoint)
        {
            _newJar = newJar.GetComponent<Container>();
            if (!_newJar.IsConteinerFull()) StartCoroutine(MoveFruitToJar(startPoint, fruit));

        }
    }
    public IEnumerator MoveVerticalFruitToPoint(Transform fruit, Vector3 target)
    {
        _fruitMove = true;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(fruit.position, target);

        while (fruit.position != target)
        {
            float distCovered = (Time.time - startTime) * _moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;

            fruit.position = Vector3.Lerp(fruit.position, target, fractionOfJourney);
            yield return null;
        }

        _fruitMove = false;
    }
    private IEnumerator MoveFruitToJar(Transform startPoint, Transform fruit)
    {
        _fruitMove = true;
        transform.position = fruit.position;
        fruit.transform.SetParent(transform);
        BezierMove bezierMove = GetComponent<BezierMove>();
        Transform newPointForMove = _newJar.transform.GetChild(2);

        yield return StartCoroutine(bezierMove.SetPointsForMove(startPoint, newPointForMove, _moveSpeed));
        if (bezierMove.toPoint) StartCoroutine(MoveFruitToLastFreePoint(fruit));
    }

    private IEnumerator MoveFruitToLastFreePoint(Transform fruit)
    {
        yield return StartCoroutine(MoveVerticalFruitToPoint(fruit, _newJar.GetEmptyPointInConteiner().position));
        _newJar.PutInConteiner(fruit);
        _newJar = null;
        _fruitMove = false;
        SoundManager.instance.PlaySoundEfects(_putInContainerSound);
    }

}
