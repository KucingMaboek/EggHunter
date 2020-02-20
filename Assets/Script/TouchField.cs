using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 touchDist;
    private Vector2 pointerOld;
    private bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchDist = Input.GetTouch(0).deltaPosition - pointerOld;
                pointerOld = Input.GetTouch(0).deltaPosition;
            }
            else
            {
                touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pointerOld;
                pointerOld = Input.mousePosition;
            }
        }
        else
        {
            touchDist = new Vector2();

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        pointerOld = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    public float Horizontal()
    {
        return touchDist.x;
    }

    public float Vertical()
    {
        return touchDist.y;
    }

}
