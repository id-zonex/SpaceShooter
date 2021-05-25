using UnityEngine;

public class BizieTest : MonoBehaviour
{
    [SerializeField] private BizieCurveMover bizieCurveMover;

    private void Update()
    {
        transform.position = bizieCurveMover.GetPoint();
        transform.localRotation = bizieCurveMover.GetRotation();
    }
}
