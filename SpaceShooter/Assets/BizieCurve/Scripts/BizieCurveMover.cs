using UnityEngine;

[System.Serializable]
public class BizieCurveMover
{
    [SerializeField] private BizieCurve bizieCurve;

    private int _currentIndex;
    private float _time;

    public Vector3 GetPoint(float speed)
    {
        _time += Time.deltaTime * speed;

        var result = bizieCurve.GetPoint(_time, _currentIndex);

        if (result.isEnd)
        {
            _currentIndex++;
            _time = 0;
        }

        if (_currentIndex >= bizieCurve.SpawnedCurveChanks.Count)
            _currentIndex = 0;

        return result.point;
    }
}