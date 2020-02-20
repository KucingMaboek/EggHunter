using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerControl player;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.Jump();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
