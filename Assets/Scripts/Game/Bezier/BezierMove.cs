using System.Collections;
using UnityEngine;

public class BezierMove : MonoBehaviour
{
    public bool toPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Vector3 _controlPoint;
    [SerializeField] private float _controlHeight;

    [Range(0, 1)]
    [SerializeField] private float _t;

    public IEnumerator SetPointsForMove(Transform startPoint, Transform endPoint, float moveSpeed)
    {
        if (startPoint && endPoint)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            UpdateControlPoint(startPoint, endPoint);
            toPoint = false;
            yield return StartCoroutine(MoveAlongCurve(startPoint, endPoint, moveSpeed));
            toPoint = true;
        }
        else
        {
            yield break;
        }
    }

    private IEnumerator MoveAlongCurve(Transform startPoint, Transform endPoint, float moveSpeed)
    {
        _t = 0f;

        while (_t < 1f)
        {
            _t += moveSpeed * Time.deltaTime;

            if (_t > 1f)
            {
                _t = 1f;
            }

            Vector3 position = Bezier.GetPoint(startPoint.position, _controlPoint, endPoint.position, _t);
            transform.position = position;
            yield return null;
        }

        _startPoint = null;
        _endPoint = null;
    }

    private void UpdateControlPoint(Transform startPoint, Transform endPoint)
    {
        Vector3 middlePoint = (_startPoint.position + _endPoint.position) / 2;
        _controlPoint = new Vector3(middlePoint.x, middlePoint.y + _controlHeight, middlePoint.z);
    }

    private void OnDrawGizmos()
    {
        if (_startPoint && _endPoint)
        {
            int segments = 20;
            Vector3 previousPoint = _startPoint.position;

            for (int i = 1; i <= segments; i++)
            {
                float t = (float)i / segments;
                Vector3 currentPoint = Bezier.GetPoint(_startPoint.position, _controlPoint, _endPoint.position, t);
                Gizmos.DrawLine(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_controlPoint, 0.1f);
        }
    }
}