using UnityEngine;

[System.Serializable]
public class BizieCurveMover
{
    [SerializeField] private NewBizieCurve bizieCurve;

    private int _currentIndex;
    private float _time;

    public Vector3 GetPoint(float speed)
    {
        _time += Time.deltaTime * speed;

        return bizieCurve.GetPoint(_time);
    }
}
