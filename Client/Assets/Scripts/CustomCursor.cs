using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // Полупрозрачная текстура круга
    public Vector2 hotSpot = new Vector2(32, 32); // Точка клика (0,0) - верхний левый угол
    public CursorMode cursorMode = CursorMode.Auto; // Режим курсора

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
        // Восстанавливаем стандартный курсор при уничтожении объекта
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
