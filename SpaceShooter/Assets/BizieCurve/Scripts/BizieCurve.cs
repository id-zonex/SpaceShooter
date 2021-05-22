using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BizieCurve : MonoBehaviour
{
    public List<BizieCurveChunk> SpawnedCurveChanks { get; private set; } = new List<BizieCurveChunk>();

    private BizieCurveChunk _bizieCurveChank;

    private BizieCurveChunk _getLastChunk => SpawnedCurveChanks[SpawnedCurveChanks.Count - 1];

    #region const
    private readonly Vector3 DEFAULT_POSITION = Vector3.zero;
    private readonly Quaternion DEFAULT_ROTATION = Quaternion.identity;
    #endregion

    private void Awake()
    {
        _bizieCurveChank = Resources.Load<BizieCurveChunk>("BizieCurveChunk");

        SetChunks();

        if (transform.childCount == 0)
            SpawnChunk();
    }

    public (Vector3 point, bool isEnd) GetPoint(float time, int currentCurve)
    {
        currentCurve = Mathf.Clamp(currentCurve, 0, SpawnedCurveChanks.Count - 1);
        BizieCurveChunk chunk = SpawnedCurveChanks[currentCurve];

        return chunk.GetPoint(time);
    }

    [EditorButton("Add")]
    public void SpawnNextChunk()
    {
        BizieCurveChunk chunk = SpawnChunk(_getLastChunk.GetLastPoint.position, Quaternion.identity, transform);

        SpawnedCurveChanks.Add(chunk);
    }

    [EditorButton("Remove")]
    public void Remove()
    {
        if (Application.isPlaying) return;

        SpawnedCurveChanks.Remove(_getLastChunk);

        _getLastChunk.gameObject.SetActive(false);
    }

    private void SetChunks()
    {
        foreach (BizieCurveChunk chunk in GetComponentsInChildren<BizieCurveChunk>().Reverse())
        {
            if (chunk.gameObject.activeInHierarchy)
                SpawnedCurveChanks.Add(chunk);
            else
                Destroy(chunk);
        }
    }

    #region SpawnChunk
    private BizieCurveChunk SpawnChunk(Vector3 position, Quaternion rotation, Transform parent)
    {
        BizieCurveChunk chunk = Instantiate(_bizieCurveChank, position, rotation, parent);
        SpawnedCurveChanks.Add(chunk);

        return chunk;
    }

    private BizieCurveChunk SpawnChunk()
    {
        BizieCurveChunk chunk = Instantiate(_bizieCurveChank, DEFAULT_POSITION, DEFAULT_ROTATION, transform);
        SpawnedCurveChanks.Add(chunk);

        return chunk;
    }
    #endregion
}
