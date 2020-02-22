using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public PlayerControl player;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.isSwitchingWeapon();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
