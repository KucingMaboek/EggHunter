using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image containerImg;
    private Image joyStickImg;

    [SerializeField] private float joystickVisualDistance = 100f;
    private Vector3 direction;
    public Vector3 Direction { get { return direction; } }
    public bool normalizedDirection;

    private void Start()
    {
        containerImg = GetComponent<Image>();
        joyStickImg = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(containerImg.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            pos.x = (pos.x / containerImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / containerImg.rectTransform.sizeDelta.y);

            Vector2 refPivot = new Vector2(0.5f, 0.5f);
            Vector2 p = containerImg.rectTransform.pivot;
            pos.x += p.x - 0.5f;
            pos.y += p.y - 0.5f;

            float x = Mathf.Clamp(pos.x, -1, 1);
            float y = Mathf.Clamp(pos.y, -1, 1);

            if (normalizedDirection)
            {
                direction = new Vector3(x, 0, y).normalized;
            }
            else
            {
                direction = new Vector3(x, 0, y);
            }

            joyStickImg.rectTransform.anchoredPosition = new Vector3(direction.x * joystickVisualDistance, direction.z * joystickVisualDistance);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        direction = Vector3.zero;
        joyStickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (direction.x != 0)
        {
            return direction.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
        if (direction.z != 0)
        {
            return direction.z;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }
}
