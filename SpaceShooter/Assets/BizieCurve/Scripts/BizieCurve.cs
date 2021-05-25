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
    
    public (Vector3 point, bool end) GetPoint(float time, int currentCurve)
    {
        BizieCurveChunk chunk = GetChunk(currentCurve);
        return chunk.GetPoint(time);
    }

    public (Vector3 derivative, bool end) GetDirection(float time, int currentCurve)
    {
        BizieCurveChunk chunk = GetChunk(currentCurve);
        return chunk.GetDirection(time);
    }

    public (Quaternion angle, bool end) GetRotation(float time, int currentCurve)
    {
        BizieCurveChunk chunk = GetChunk(currentCurve);
        return chunk.GetRotation(time);
    }

    #region Editor Buttons
    public void SpawnNextChunk()
    {
        SpawnChunk(_getLastChunk.GetLastPoint);
    }

    public void Remove()
    {
        spawnedCurveChanks.Remove(_getLastChunk);
    }
    #endregion

    #region SpawnChunk
    private BizieCurveChunk SpawnChunk(Vector3 position)
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
    #endregion

    private BizieCurveChunk GetChunk(int currentCurve)
    {
        currentCurve = Mathf.Clamp(currentCurve, 0, spawnedCurveChanks.Count - 1);
        BizieCurveChunk chunk = spawnedCurveChanks[currentCurve];

        return chunk;
    }

    private void CheckChunks()
    {
        if (spawnedCurveChanks.Count == 0)
            SpawnChunk();
    }

    private void OnDrawGizmos()
    {
        foreach(BizieCurveChunk chunk in spawnedCurveChanks)
        {
            chunk.DrawGizmo();
        }
    }
}
