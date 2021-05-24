using System.Collections;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class BizieCurveChunk
{
    #region Editor fields
    public Vector3 P1 = new Vector3(0, 0, 0);
    public Vector3 P2 = new Vector3(10.5f, 11.8f, 0);
    public Vector3 P3 = new Vector3(-10.9f, 8.5f, 0);
    public Vector3 P4 = new Vector3(0, 20, 0);
    #endregion

    #region lambdes
    public Vector3 GetLastPoint => P4;
    #endregion

    private Vector3 _pivot = new Vector3(0, 0, 0);

    #region Constructors
    public BizieCurveChunk(Vector3 pivot)
    {
        _pivot = pivot;
        Initialize();
    }

    public BizieCurveChunk() { }
    #endregion

    public void Initialize()
    {
        P1 = P1 + _pivot;
        P2 = P2 + _pivot;
        P3 = P3 + _pivot;
        P4 = P4 + _pivot;
    }

    #region Public bezie methods
    public (Vector3 point, bool isEnd) GetPoint(float time)
    {
        Vector3 point = GetPointInDistance(time);
        bool isEnd = HasEnd(point);

        return (point, isEnd);
    }

    public bool HasEnd(Vector3 position)
    {
        return Vector3.Distance(position, P4) == 0 ? true : false;
    }

    public Quaternion GetRotation(float time)
    {
        return Quaternion.LookRotation(GetFirstDerivative(time));
    }

    public Vector3 GetFirstDerivative(float time)
    {
        time = Mathf.Clamp01(time);
        float oneMinusT = 1f - time;

        return
            3f * oneMinusT * oneMinusT * (P2 - P1) +
            6f * oneMinusT * time * (P3 - P2) +
            3f * time * time * (P4 - P3);
    }
    #endregion

    #region Editor
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
    #endregion

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
}