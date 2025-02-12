using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform background; // База джойстика
    public RectTransform stick;     // Стик джойстика

    private Vector2 startPos;

    void Start()
    {
        startPos = stick.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Ничего не делаем здесь, если нужно
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out pos))
        {
            // Ограничиваем движение стика внутри круга
            float radius = background.sizeDelta.x / 2 - stick.sizeDelta.x / 2;
            pos = ClampToCircle(pos, radius);

            stick.anchoredPosition = pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stick.anchoredPosition = startPos;
    }

    public Vector2 GetInputDirection()
    {
        Vector2 inputDir = (stick.anchoredPosition - startPos).normalized;
        return inputDir;
    }

    private Vector2 ClampToCircle(Vector2 pos, float radius)
    {
        float distance = Vector2.Distance(Vector2.zero, pos);
        if (distance > radius)
        {
            pos = pos.normalized * radius;
        }
        return pos;
    }
}