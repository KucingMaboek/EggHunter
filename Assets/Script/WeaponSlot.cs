using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public GameObject rotationPoint;
    public GameObject noTarget;
    public Camera fpsCam;
    public GameObject currentWeapon;
    public GameObject weaponDrop;
    public string currentWeaponName;
    public RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        initiateWeapon();
        if (transform.parent.name == "Player")
        {
            transform.parent.GetComponent<PlayerControl>().setWeaponSlot(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fpsCam != null)
        {
            Transform raycastStartPoint = fpsCam.transform.Find("RaycastStartPoint").transform;
            Physics.Raycast(raycastStartPoint.position, raycastStartPoint.transform.forward, out hit);
            Vector3 temp = transform.eulerAngles;
            temp.x = rotationPoint.transform.eulerAngles.x;
            transform.rotation = Quaternion.Euler(temp);
        }
    }

    private bool targetObjectCheck()
    {
        try
        {
            Debug.Log(hit.transform.name);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    public void changeWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null)
        {
            dropWeapon();

            currentWeapon = newWeapon;
            string temp = currentWeapon.transform.name;
            currentWeapon = Instantiate(currentWeapon, transform.position, transform.rotation);
            currentWeapon.transform.parent = gameObject.transform;
            currentWeapon.transform.name = temp;
            currentWeapon.GetComponent<WeaponControl>().enabled = true;
            currentWeaponName = currentWeapon.transform.name;

            if (transform.parent != null)
            {
                if (transform.parent.tag == "Player")
                {
                    transform.parent.GetComponent<PlayerControl>().assignWeapon(currentWeapon);
                }
                else //If enemy.
                {
                    transform.parent.GetComponent<Enemy>().assignWeapon(currentWeapon);
                }
            }
        }
        else //if currentWeapon is null.
        {
            currentWeapon = newWeapon;
            string temp = currentWeapon.transform.name;
            currentWeapon = Instantiate(currentWeapon, transform.position, transform.rotation);
            currentWeapon.transform.parent = gameObject.transform;
            currentWeapon.transform.name = temp;
            currentWeaponName = currentWeapon.transform.name;

            if (transform.parent != null)
            {
                if (transform.parent.tag == "Player")
                {
                    transform.parent.GetComponent<PlayerControl>().assignWeapon(currentWeapon);
                }
                else //If enemy.
                {
                    transform.parent.GetComponent<Enemy>().assignWeapon(currentWeapon);
                }
            }
        }
    }

    public void initiateWeapon()
    {
        if (currentWeapon != null)
        {
            string temp = currentWeapon.transform.name;
            currentWeapon = Instantiate(currentWeapon, transform.position, transform.rotation);
            currentWeapon.transform.parent = gameObject.transform;
            currentWeapon.transform.name = temp;
            currentWeaponName = currentWeapon.transform.name;

            if (transform.parent != null)
            {
                if (transform.parent.tag == "Player")
                {
                    transform.parent.GetComponent<PlayerControl>().assignWeapon(currentWeapon);
                }
                else //If enemy.
                {
                    transform.parent.GetComponent<Enemy>().assignWeapon(currentWeapon);
                }
            }
        }
    }

    public void dropWeapon()
    {
        if (currentWeapon != null)
        {
            GameObject weaponDropped = Instantiate(weaponDrop, transform.position, Quaternion.Euler(0, 0, 0));
            weaponDropped.GetComponent<WeaponDrop>().setWeapon(currentWeapon);
            Destroy(currentWeapon);
        }
    }
}
