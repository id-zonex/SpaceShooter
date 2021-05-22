using UnityEngine;

public class BizieCurveChunk : MonoBehaviour
{
    #region Editor fields
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;
    [SerializeField] private Transform p3;
    [SerializeField] private Transform p4;
    #endregion

    public Transform GetLastPoint => p4;

    public (Vector3 point, bool isEnd) GetPoint(float time)
    {
        Vector3 point = GetPointInDistance(time);
        bool isEnd = HasEnd(point);

        return (point, isEnd);
    }

    public bool HasEnd(Vector3 position)
    {
        return Vector3.Distance(position, p4.position) == 0 ? true : false;
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

    private Vector3 GetPointInDistance(float time)
    {
        time = Mathf.Clamp01(time);
        float oneMinusT = 1f - time;

        Vector3 point = oneMinusT * oneMinusT * oneMinusT * p1.position +
            3f * oneMinusT * oneMinusT * time * p2.position +
            3f * oneMinusT * time * time * p3.position +
            time * time * time * p4.position;

        return point;
    }

    private void OnDrawGizmos()
    {
        int sigmentsNumber = 20;
        Vector3 preveousePoint = p1.position;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float)i / sigmentsNumber;
            Vector3 point = GetPointInDistance(paremeter);

            Gizmos.DrawLine(preveousePoint, point);

            preveousePoint = point;
        }
    }
}
