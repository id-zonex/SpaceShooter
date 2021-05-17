using System.Collections.Generic;
using UnityEngine;


public class NewBizieCurve : MonoBehaviour
{
    [SerializeField] private float xOffset = 5;
    [SerializeField] private float yOffset = 2;
    [SerializeField] private float zOffset = 0;

    [SerializeField] private List<Transform> points;

    private Transform _getLastPoint => points[points.Count - 1];

    public Vector3 GetPoint(float t, int iL=0, int iR=-1)
    {
        if (iR == -1)
            iR = points.Count - 1;

        if (iL == iR)
            return points[iL].position;

        Vector3 lV3 = GetPoint(t, iL, iR - 1);
        Vector3 rV3 = GetPoint(t, iL + 1, iR);

        Vector3 result = Vector3.Lerp(lV3, rV3, t);

        return result;
    }

    [EditorButton("Add")]
    public void SpawnSegment()
    {
        Transform point = CreateSegment("Point");

        points.Add(point);
    }

    [EditorButton("Remove")]
    public void RemoveSegment()
    {
        points.Remove(_getLastPoint);

        if (Application.isPlaying)
            Destroy(_getLastPoint);
    }

    private Transform CreateSegment(string name)
    {
        Vector3 lastPoint = transform.position;

        try
        {
            lastPoint = _getLastPoint.position;
        }
        catch { }


        Transform obj = new GameObject(name).transform;

        Vector3 position = new Vector3
            (
            lastPoint.x + xOffset,
            lastPoint.y + yOffset,
            lastPoint.z + zOffset
            );

        obj.position = position;
        obj.SetParent(transform);

        return obj;
    }

    private void OnDrawGizmos()
    {
        if (points.Count < 1) return;

        int sigmentsNumber = 10;
        Vector3 preveousePoint = points[0].position;

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float)i / sigmentsNumber;
            Vector3 point = GetPoint(paremeter);

            Gizmos.DrawLine(preveousePoint, point);

            preveousePoint = point;
        }
    }
}
