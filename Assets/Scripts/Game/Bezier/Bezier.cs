using UnityEngine;

public class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        Vector3 point = oneMinusT * oneMinusT * p0 +
                        2f * oneMinusT * t * p1 +
                        t * t * p2;

        return point;
    }
}
