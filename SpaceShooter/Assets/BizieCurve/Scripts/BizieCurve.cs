using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BizieCurve : MonoBehaviour
{
    [SerializeField] private BizieCurveChunk bizieCurveChank;

    private List<BizieCurveChunk> _bizieCurveChanks = new List<BizieCurveChunk>();
    private BizieCurveChunk _getLastChunk => _bizieCurveChanks[_bizieCurveChanks.Count - 1];

    #region const
    private readonly Vector3 DEFAULT_POSITION = Vector3.zero;
    private readonly Quaternion DEFAULT_ROTATION = Quaternion.identity;
    #endregion

    private void Awake()
    {
        bizieCurveChank = BizieCurvePrefab.BizieCurveChunkPrefab;
        SetChunks();

        if (transform.childCount == 0)
        {
            SpawnChunk();
        }
    }

    public Vector3 GetPoint(float time)
    {
        int currentIndex = (int)(time * 10);

        currentIndex = Mathf.Clamp(currentIndex, 0, _bizieCurveChanks.Count - 1);

        float chunkTime = (time / _bizieCurveChanks.Count);
        BizieCurveChunk chunk = _bizieCurveChanks[currentIndex];

        Vector3 point = chunk.GetPoint(chunkTime);

        return point;
    }


    public void SpawnNextChunk()
    {
        BizieCurveChunk chunk;

        chunk = SpawnChunk(_getLastChunk.GetLastPoint().position, Quaternion.identity, transform);

        _bizieCurveChanks.Add(chunk);
    }

    private void SetChunks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _bizieCurveChanks.Add(transform.GetChild(i).GetComponent<BizieCurveChunk>());
        }
    }

    #region SpawnChunk
    private BizieCurveChunk SpawnChunk(Vector3 position, Quaternion rotation, Transform parent)
    {
        BizieCurveChunk chunk = Instantiate(bizieCurveChank, position, rotation, parent);
        _bizieCurveChanks.Add(chunk);

        return chunk;
    }

    private void SpawnChunk()
    {
        BizieCurveChunk chunk = Instantiate(bizieCurveChank, DEFAULT_POSITION, DEFAULT_ROTATION, transform);
        _bizieCurveChanks.Add(chunk);
    }
    #endregion
}
