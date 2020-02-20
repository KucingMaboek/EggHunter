using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReloadButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerControl player;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.isReload();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
