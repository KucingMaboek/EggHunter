using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerControl player;
    public void OnPointerDown(PointerEventData eventData)
    {
        player.isSwitchingWeapon();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
