using UnityEngine;
using DG.Tweening;

public class RotateSelf : MonoBehaviour
{
    public float rotateDuration = 2f; // Продолжительность вращения (в секундах)
    public float rotateAngle = 360f; // Угол вращения (по умолчанию - полный оборот)

    void Start()
    {
        // Вызываем метод для вращения объекта
        RotateObject();
    }

    void RotateObject()
    {
        // Используем DOTween для плавного вращения объекта вокруг себя
        transform.DORotate(new Vector3(0, 0, rotateAngle), rotateDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart) // Зацикливаем вращение
            .SetEase(Ease.Linear); // Задаем линейное затухание (постоянную скорость)
    }
}