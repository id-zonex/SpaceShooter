using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BizieCurve : MonoBehaviour
{
    [SerializeField] private BizieCurveChunk bizieCurveChank;

    public List<BizieCurveChunk> BizieCurveChanks { get; private set; } = new List<BizieCurveChunk>();
    private BizieCurveChunk _getLastChunk => BizieCurveChanks[BizieCurveChanks.Count - 1];

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

    public Vector3 GetPoint(float time, int currentCurve, out bool isEnd)
    {
        currentCurve = Mathf.Clamp(currentCurve, 0, BizieCurveChanks.Count - 1);

        float chunkTime = time;

        BizieCurveChunk chunk = BizieCurveChanks[currentCurve];

        Vector3 point = chunk.GetPoint(chunkTime, out isEnd);

        return point;
    }

    [EditorButton("Add")]
    public void SpawnNextChunk()
    {
        BizieCurveChunk chunk;

        chunk = SpawnChunk(_getLastChunk.GetLastPoint().position, Quaternion.identity, transform);

        BizieCurveChanks.Add(chunk);
    }

    [EditorButton("Remove")]
    public void Remove()
    {
        BizieCurveChanks.Remove(_getLastChunk);

        if (Application.isPlaying)
            Destroy(_getLastChunk);
    }

    private void SetChunks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BizieCurveChanks.Add(transform.GetChild(i).GetComponent<BizieCurveChunk>());
        }
    }

    #region SpawnChunk
    private BizieCurveChunk SpawnChunk(Vector3 position, Quaternion rotation, Transform parent)
    {
        BizieCurveChunk chunk = Instantiate(bizieCurveChank, position, rotation, parent);
        BizieCurveChanks.Add(chunk);

        return chunk;
    }

    private void SpawnChunk()
    {
        BizieCurveChunk chunk = Instantiate(bizieCurveChank, DEFAULT_POSITION, DEFAULT_ROTATION, transform);
        BizieCurveChanks.Add(chunk);
    }
    #endregion
}
