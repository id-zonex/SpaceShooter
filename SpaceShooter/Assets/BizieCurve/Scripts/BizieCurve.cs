using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BizieCurve : MonoBehaviour
{
    [SerializeField] private List<BizieCurveChunk> spawnedCurveChanks = new List<BizieCurveChunk>();
    public List<BizieCurveChunk> SpawnedCurveChanks => spawnedCurveChanks;

    private BizieCurveChunk _getLastChunk => spawnedCurveChanks[spawnedCurveChanks.Count - 1];

    private void Start()
    {
        CheckChunks();
    }

    public (Vector3 point, bool isEnd) GetPoint(float time, int currentCurve)
    {
        currentCurve = Mathf.Clamp(currentCurve, 0, spawnedCurveChanks.Count - 1);
        BizieCurveChunk chunk = spawnedCurveChanks[currentCurve];

        return chunk.GetPoint(time);
    }

    #region Editor Buttons
    public void SpawnNextChunk()
    {
        SpawnChunk(_getLastChunk.GetLastPoint, Quaternion.identity, transform);
    }

    public void Remove()
    {
        spawnedCurveChanks.Remove(_getLastChunk);
    }
    #endregion

    #region SpawnChunk
    private BizieCurveChunk SpawnChunk(Vector3 position, Quaternion rotation, Transform parent)
    {
        BizieCurveChunk chunk = new BizieCurveChunk(position);
        spawnedCurveChanks.Add(chunk);

        return chunk;
    }

    private BizieCurveChunk SpawnChunk()
    {
        BizieCurveChunk chunk = new BizieCurveChunk(transform.position);
        spawnedCurveChanks.Add(chunk);

        return chunk;
    }

    private void CheckChunks()
    {
        if (spawnedCurveChanks.Count == 0)
            SpawnChunk();
    }
    #endregion

    private void OnDrawGizmos()
    {
        foreach(BizieCurveChunk chunk in spawnedCurveChanks)
        {
            chunk.DrawGizmo();
        }
    }
}
