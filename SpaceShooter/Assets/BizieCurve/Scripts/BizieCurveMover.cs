using UnityEngine;

[System.Serializable]
public class BizieCurveMover
{
    [SerializeField] private BizieCurve bizieCurve;
    [SerializeField] private float speed = 1;

    private int _currentIndex;
    private float _time;

    public Vector3 GetPoint()
    {
        _time += Time.deltaTime * speed;
        var result = bizieCurve.GetPoint(_time, _currentIndex);

        UpdateCurveIndex(result.end);

        return result.point;
    }

    public Quaternion GetRotation()
    {
        _time += Time.deltaTime * speed;
        var result = bizieCurve.GetRotation(_time, _currentIndex);

        UpdateCurveIndex(result.end);

        return new Quaternion(0, 0, result.angle.z, result.angle.w);
    }

    private void UpdateCurveIndex(bool end)
    {
        if (end)
        {
            _currentIndex++;
            _time = 0;
        }

        if (_currentIndex >= bizieCurve.SpawnedCurveChanks.Count)
            _currentIndex = 0;
    }
}