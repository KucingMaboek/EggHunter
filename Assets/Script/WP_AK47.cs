using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WP_AK47 : MonoBehaviour, IWeaponControl
{
    public GameObject projectile;
    private Transform shotPoint;
    public float damage = 3;
    private float cooldown;
    private float fireRate = 0.1f;
    public int currentAmmo;
    public int stockAmmo = 120;
    private int ammoCapacity = 30;
    private float reloadTime = 1.1f;
    private float currentReloadTime = 0f;
    private bool isReloading;
    private ReloadButton reloadUI;
    Text ammoUI;
    private Transform rootParent;

    public void AddAmmo(int ammo)
    {
        stockAmmo += ammo;
    }

    public void WeaponCooldown()
    {
        if (cooldown > 0 || !isReloading)
        {
            cooldown -= Time.deltaTime;
        }

        if (currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
            reloadUI.CurrentReloadTime(currentReloadTime);
            isReloading = true;
        }
        else
        {
            isReloading = false;
        }
    }

    public void Fire()
    {
        if (cooldown <= 0 && !isReloading)
        {
            if (currentAmmo > 0)
            {
                GameObject activeProjectile = Instantiate(projectile, shotPoint.position, transform.rotation);
                activeProjectile.transform.GetComponent<Bullet>().setDamage(damage);
                activeProjectile.transform.GetComponent<Bullet>().setNoTarget(transform.parent.parent.Find("RotatingPoint").Find("NoTarget"));
                activeProjectile.transform.parent = transform.parent;
                currentAmmo -= 1;
                cooldown = fireRate;
            }
        }
    }

    public void Reload()
    {
        int temp;
        if (stockAmmo > 0 && !isReloading)
        {
            temp = ammoCapacity - currentAmmo;
            if (stockAmmo >= temp)
            {
                stockAmmo -= temp;
                currentAmmo += temp;
                currentReloadTime = reloadTime;
            }
            else
            {
                currentAmmo += stockAmmo;
                stockAmmo = 0;
                currentReloadTime = reloadTime;
            }
        }
        else
        {
            //play sound
            Debug.Log("Not Enough Ammo");
        }
    }

    public void ShowAmmoOnUI()
    {
        ammoUI.text = currentAmmo + "/" + stockAmmo;
    }

    public void TriggerOnHold()
    {
        Fire();
    }

    public void TriggerOnRelease(){}

    public void CheckForRootParent()
    {
        try
        {
            rootParent = transform.parent.parent;
        }
        catch (System.Exception e)
        {
            string exception = e.Message;
        }
    }

    public int GetStockAmmo()
    {
        return stockAmmo;
    }

    public void InitializeSP()
    {
        shotPoint = transform.GetChild(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckForRootParent();
        if (rootParent != null && rootParent.tag == "Player")
        {
            ammoUI = rootParent.Find("MainCanvas").Find("AmmunitionUI").GetComponent<Text>();
            reloadUI = rootParent.Find("MainCanvas").Find("ReloadButton").GetComponent<ReloadButton>();
            reloadUI.SetReloadValue(reloadTime);
            Reload();
        }
        InitializeSP();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponCooldown();
    }
}
