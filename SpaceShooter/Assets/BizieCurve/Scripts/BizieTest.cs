using UnityEngine;

public class BizieTest : MonoBehaviour
{
    [SerializeField] BizieCurve curve;
    [SerializeField] float speed = 2;

    private float _time;

    private void Update()
    {
        transform.position = curve.GetPoint(_time);
        _time += Time.deltaTime * speed;
    }
}
