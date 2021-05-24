using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BizieCurve))]
public class BezieEditor : Editor
{
    private BizieCurve _target => (BizieCurve)base.target;

    private void OnSceneGUI()
    {
        DrawChunks();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawButtons();
    }

    #region Draw Chunks
    private void DrawChunks()
    {
        foreach (BizieCurveChunk chunk in _target.SpawnedCurveChanks)
        {
            DrawChunk(chunk);
        }
    }

    private void DrawChunk(BizieCurveChunk chunk)
    {
        EditorGUI.BeginChangeCheck();

        Vector3 p1 = Handles.PositionHandle(chunk.P1, Quaternion.identity);
        Vector3 p2 = Handles.PositionHandle(chunk.P2, Quaternion.identity);
        Vector3 p3 = Handles.PositionHandle(chunk.P3, Quaternion.identity);
        Vector3 p4 = Handles.PositionHandle(chunk.P4, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changet points");

            chunk.P1 = p1;
            chunk.P2 = p2;
            chunk.P3 = p3;
            chunk.P4 = p4;
        }
    }
    #endregion

    private void DrawButtons()
    {
        if(GUILayout.Button("Add"))
            _target.SpawnNextChunk();

        else if(GUILayout.Button("Remove"))
            _target.Remove();
    }
}
