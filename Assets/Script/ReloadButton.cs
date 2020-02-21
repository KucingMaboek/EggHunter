using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReloadButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image reloadBar;
    public float reloadValue = 0f;
    public float currentReloadTime = 0f;
    public PlayerControl player;


    private void Start()
    {
        reloadBar = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        ReloadCounter();
        var enableSliderBar = currentReloadTime <= 0 ? reloadBar.enabled = false : reloadBar.enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player.isReload();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    private void ReloadCounter()
    {
        float amount = (currentReloadTime/reloadValue);
        reloadBar.fillAmount = amount;
    }

    public void SetReloadValue(float reloadTime)
    {
        reloadValue = reloadTime;
    }

    public void CurrentReloadTime(float currentReloadTime)
    {
        this.currentReloadTime = currentReloadTime;
    }
}
