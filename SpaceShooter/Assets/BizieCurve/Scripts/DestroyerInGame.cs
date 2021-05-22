using UnityEngine;

/// <summary>
/// ���� ��������� ����� ������ �� "���������" �� ����� ������� ������ ��������
/// </summary>
public class DestroyerInGame : MonoBehaviour
{
    [EditorButton("Clear")]
    public void Clear()
    {
        if (Application.isEditor) return;

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }
    }
}
