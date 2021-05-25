using UnityEngine;

public class BizieTest : MonoBehaviour
{
    [SerializeField] private BizieCurveMover bizieCurveMover;
    [SerializeField] private float speed = 1;

    private void Start()
    {
        speed = Random.Range(1f, 5f);
    }

    private void Update()
    {
        transform.position = bizieCurveMover.GetPoint(speed);
    }
}
