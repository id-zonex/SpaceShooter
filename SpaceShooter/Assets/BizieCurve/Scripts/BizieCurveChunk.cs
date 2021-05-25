using UnityEngine;
using UnityEditor;

[System.Serializable]
public class BizieCurveChunk
{
    #region Points
    public Vector3 P1 = new Vector3(0, 0, 0);
    public Vector3 P2 = new Vector3(10.5f, 11.8f, 0);
    public Vector3 P3 = new Vector3(-10.9f, 8.5f, 0);
    public Vector3 P4 = new Vector3(0, 20, 0);
    #endregion

    public Vector3 GetLastPoint => P4;

    private Vector3 _pivot = new Vector3(0, 0, 0);

    #region Constructor
    public BizieCurveChunk(Vector3 pivot)
    {
        _pivot = pivot;
        Initialize();
    }

    public void Initialize()
    {
        P1 = P1 + _pivot;
        P2 = P2 + _pivot;
        P3 = P3 + _pivot;
        P4 = P4 + _pivot;
    }
    #endregion

    public bool HasEnd(float time)
    {
        return GetPoint(time).end;
    }

    public bool HasEnd(Vector3 position)
    {
        return Vector3.Distance(position, P4) == 0 ? true : false;
    }

    public (Vector3 point, bool end) GetPoint(float time)
    {
        Vector3 point = GetPointInDistance(time);
        bool end = HasEnd(point);

        return (point, end);
    }

    public (Quaternion angle, bool end) GetRotation(float time)
    {
        Quaternion angle = Quaternion.LookRotation(GetFirstDerivative(time));
        bool end = HasEnd(time);

        return (angle, end);
    }

    public (Vector3 direction, bool end) GetDirection(float time)
    {
        Vector3 direction = GetFirstDerivative(time);
        bool end = HasEnd(time);

        return (direction, end);
    }


    private Vector3 GetFirstDerivative(float time)
    {
        time = Mathf.Clamp01(time);
        float oneMinusT = 1f - time;

        Vector3 derivative = 3f * oneMinusT * oneMinusT * (P2 - P1) +
            6f * oneMinusT * time * (P3 - P2) +
            3f * time * time * (P4 - P3);

        return derivative;
    }

    private Vector3 GetPointInDistance(float time)
    {
        time = Mathf.Clamp01(time);
        float oneMinusT = 1f - time;

        Vector3 point = oneMinusT * oneMinusT * oneMinusT * P1 +
            3f * oneMinusT * oneMinusT * time * P2 +
            3f * oneMinusT * time * time * P3 +
            time * time * time * P4;

        return point;
    }

    [DrawGizmo(GizmoType.Active)]
    public void DrawGizmo()
    {
        int sigmentsNumber = 20;
        Vector3 preveousePoint = P1;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float)i / sigmentsNumber;
            Vector3 point = GetPointInDistance(paremeter);

            Gizmos.DrawLine(preveousePoint, point);

            preveousePoint = point;
        }
    }
}