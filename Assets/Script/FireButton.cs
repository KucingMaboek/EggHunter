using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public PlayerControl player;
    public float deadZone = 0.1f;
    public float aimSensitivity = 1f;
    private bool continousTrigger;
    private Image containerImg;
    private Image joyStickImg;

    [SerializeField] private float joystickVisualDistance = 100f;
    private Vector3 direction;
    public Vector3 Direction { get { return direction; } }

    private bool isFiring;


    private void Start()
    {
        containerImg = GetComponent<Image>();
        joyStickImg = transform.GetChild(1).GetComponent<Image>();
    }

    private void Update()
    {
        if (isFiring)
        {
            player.isShoot();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        isFiring = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isFiring = false;

        direction = Vector3.zero;
        joyStickImg.rectTransform.anchoredPosition = Vector3.zero;
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

            direction = new Vector3(x, 0, y);

            joyStickImg.rectTransform.anchoredPosition = new Vector3(direction.x * joystickVisualDistance, direction.z * joystickVisualDistance);
        }
    }

    public float Horizontal()
    {
        if (direction.x >= deadZone)
        {
            return direction.x * aimSensitivity;
        }
        else if (direction.x <= -deadZone)
        {
            return direction.x * aimSensitivity;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
        if (direction.z >= deadZone)
        {
            return direction.z * aimSensitivity;
        }
        else if (direction.z <= -deadZone)
        {
            return direction.z * aimSensitivity;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }

    public bool IsFiring()
    {
        return isFiring;
    }
}
