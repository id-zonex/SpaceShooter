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

        Vector3 point = bizieCurve.GetPoint(_time, _currentIndex, out bool isEnd);

        if (isEnd)
        {
            _currentIndex++;
            _time = 0;
        }

        if (_currentIndex >= bizieCurve.BizieCurveChanks.Count)
            _currentIndex = 0;

        return point;
    }
}
