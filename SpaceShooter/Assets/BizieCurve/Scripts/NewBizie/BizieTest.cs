using UnityEngine;

public class BizieTest : MonoBehaviour
{
    [SerializeField] private BizieCurveMover bizieCurveMover;
    [SerializeField] float speed = 2;

    private void Update()
    {
        transform.position = bizieCurveMover.GetPoint(speed);
    }
}
