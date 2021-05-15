using UnityEngine;

[CreateAssetMenu()]
public class BizieCurvePrefab : ScriptableObject
{
    [SerializeField] private BizieCurveChunk bizieCurveChunkPrefab;

    public static BizieCurveChunk BizieCurveChunkPrefab;

    private void OnValidate()
    {
        SetStaticBizieCurve();
    }

    private void OnEnable()
    {
        SetStaticBizieCurve();
    }

    private void SetStaticBizieCurve()
    {
        BizieCurveChunkPrefab = bizieCurveChunkPrefab;
    }
}
