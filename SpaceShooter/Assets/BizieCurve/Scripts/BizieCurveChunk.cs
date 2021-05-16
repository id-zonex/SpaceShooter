using UnityEngine;

public class BizieCurveChunk : MonoBehaviour
{
    #region Editor fields
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;
    [SerializeField] private Transform p3;
    [SerializeField] private Transform p4;
    #endregion

    public Vector3 GetPoint(float time, out bool isEnd)
    {
        isEnd = false;
        Vector3 point = GetPoint(time);

        if (Vector3.Distance(point, p4.position) == 0)
            isEnd = true;

        return point;
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
            3f * oneMinusT * oneMinusT * (p2.position - p1.position) +
            6f * oneMinusT * time * (p3.position - p2.position) +
            3f * time * time * (p4.position - p3.position);
    }

    private Vector3 GetPoint(float time)
    {
        time = Mathf.Clamp01(time);
        float oneMinusT = 1f - time;

        Vector3 point = oneMinusT * oneMinusT * oneMinusT * p1.position +
            3f * oneMinusT * oneMinusT * time * p2.position +
            3f * oneMinusT * time * time * p3.position +
            time * time * time * p4.position;

        return point;
    }

    public Transform GetLastPoint() { return p4; }

    public Transform GetFirstPoint() { return p1; }

    private void OnDrawGizmos()
    {
        int sigmentsNumber = 20;
        Vector3 preveousePoint = p1.position;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float)i / sigmentsNumber;
            Vector3 point = GetPoint(paremeter);

            Gizmos.DrawLine(preveousePoint, point);

            preveousePoint = point;
        }
    }
}
