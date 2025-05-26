using UnityEngine;

public class Spline : MonoBehaviour
{
    [Header("Spline Noktalarý (en az 4)")]
    public Transform[] controlPoints;

    public Vector3 GetPoint(float t)
    {
        if (controlPoints == null || controlPoints.Length < 4)
        {
            Debug.LogError("Spline çalýþmasý için en az 4 control point gerekli!");
            return Vector3.zero;
        }

        int numSections = controlPoints.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);
        float u = t * numSections - currPt;

        Vector3 a = controlPoints[currPt].position;
        Vector3 b = controlPoints[currPt + 1].position;
        Vector3 c = controlPoints[currPt + 2].position;
        Vector3 d = controlPoints[currPt + 3].position;

        return 0.5f * (
            (-a + 3f * b - 3f * c + d) * (u * u * u) +
            (2f * a - 5f * b + 4f * c - d) * (u * u) +
            (-a + c) * u +
            2f * b
        );
    }

    private void OnDrawGizmos()
    {
        if (controlPoints == null || controlPoints.Length < 4) return;

        Gizmos.color = Color.cyan;
        Vector3 prev = GetPoint(0f);

        for (float t = 0; t <= 1f; t += 0.01f)
        {
            Vector3 pos = GetPoint(t);
            Gizmos.DrawLine(prev, pos);
            prev = pos;
        }
    }
}
