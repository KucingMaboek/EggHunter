using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject projectile;
    public GameObject shotPoint;
    public GameObject currentSP;
    public float damage;
    public float shootDelay;
    [SerializeField] private float fireRate = 0f;
    public int currentAmmo;
    public int stockAmmo;
    [SerializeField] private int ammoCapacity = 0;
    [SerializeField] private float shotPointPosX = 0f;
    [SerializeField] private float shotPointPosY = 0f;
    [SerializeField] private float shotPointPosZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Reload();
        if (currentSP == null)
        {
            Vector3 temp = new Vector3(
                transform.position.x + shotPointPosX,
                transform.position.y + shotPointPosY,
                transform.position.z + shotPointPosZ);
            currentSP = Instantiate(shotPoint, temp, transform.rotation);
            currentSP.transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shootDelay > 0)
        {
            shootDelay -= Time.deltaTime;
        }
    }
    
    public void Shoot()
    {
        Debug.Log("2 step");
        if (shootDelay <= 0)
        {
            Debug.Log("1step");
            if (currentAmmo > 0)
            {
                Debug.Log("player nembak");
                GameObject activeProjectile = Instantiate(projectile, currentSP.transform.position, transform.rotation);
                activeProjectile.transform.GetComponent<Bullet>().setDamage(damage);
                activeProjectile.transform.GetComponent<Bullet>().setNoTarget(transform.parent.parent.Find("RotatingPoint").Find("NoTarget"));
                activeProjectile.transform.parent = transform.parent;
                //activeProjectile.transform.GetComponents<Bullet>()
                currentAmmo -= 1;
                shootDelay = fireRate;
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
        } else
        {
            //play sound
            Debug.Log("Not Enough Ammo");
        }
    }

    public void sumAmmo(int ammo)
    {
        stockAmmo += ammo;
    }    
}
