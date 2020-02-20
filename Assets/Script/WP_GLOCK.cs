using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WP_GLOCK : MonoBehaviour, IWeaponControl
{
    public GameObject projectile;
    private Transform shotPoint;
    public float damage = 5;
    private float cooldown;
    private float fireRate = 0.5f;
    public int currentAmmo;
    public int stockAmmo = 60;
    private int ammoCapacity = 12;
    Text ammoUI;
    private Transform rootParent;

    public void AddAmmo(int ammo)
    {
        stockAmmo += ammo;
    }

    public void WeaponCooldown()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (cooldown <= 0)
        {
            if (currentAmmo > 0)
            {
                GameObject activeProjectile = Instantiate(projectile, shotPoint.position, transform.rotation);
                activeProjectile.transform.GetComponent<Bullet>().setDamage(damage);
                activeProjectile.transform.GetComponent<Bullet>().setNoTarget(transform.parent.parent.Find("RotatingPoint").Find("NoTarget"));
                activeProjectile.transform.parent = transform.parent;
                //activeProjectile.transform.GetComponents<Bullet>()
                currentAmmo -= 1;
                cooldown = fireRate;
            }
        }
    }

    public void Reload()
    {
        int temp;
        if (stockAmmo > 0)
        {
            temp = ammoCapacity - currentAmmo;
            if (stockAmmo >= temp)
            {
                stockAmmo -= temp;
                currentAmmo += temp;
            }
            else
            {
                currentAmmo += stockAmmo;
                stockAmmo = 0;
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

    public void TriggerOnRelease() { }

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
        if (rootParent != null && rootParent.name == "Player")
        {
            ammoUI = rootParent.Find("MainCanvas").Find("AmmunitionUI").GetComponent<Text>();
        }
        Reload();
        InitializeSP();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponCooldown();
    }


}
