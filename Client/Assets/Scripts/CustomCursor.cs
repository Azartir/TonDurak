using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // �������������� �������� �����
    public Vector2 hotSpot = new Vector2(32, 32); // ����� ����� (0,0) - ������� ����� ����
    public CursorMode cursorMode = CursorMode.Auto; // ����� �������

    void Start()
    {
        if (cursorTexture == null)
        {
            Debug.LogError("Cursor texture is not assigned. Please assign a texture in the inspector.");
            return;
        }

        SetCustomCursor();
    }

    private void SetCustomCursor()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    private void OnDestroy()
    {
        // ��������������� ����������� ������ ��� ����������� �������
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
