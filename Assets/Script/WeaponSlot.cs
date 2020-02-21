using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public GameObject rotationPoint;
    public GameObject noTarget;
    public Camera tpsCam;
    public GameObject currentWeapon;
    public GameObject weaponDrop;
    public string currentWeaponName;
    public RaycastHit hit;
    private Transform rootParent;


    // Start is called before the first frame update
    void Start()
    {
        rootParent = transform.parent;
        InitiateWeapon();
        if (rootParent.tag == "Player")
        {
            rootParent.GetComponent<PlayerControl>().setWeaponSlot(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tpsCam != null)
        {
            Transform raycastStartPoint = tpsCam.transform.Find("RaycastStartPoint").transform;
            Physics.Raycast(raycastStartPoint.position, raycastStartPoint.transform.forward, out hit);
            Vector3 temp = transform.eulerAngles;
            temp.x = rotationPoint.transform.eulerAngles.x;
            transform.rotation = Quaternion.Euler(temp);
        }
    }

    private bool TargetObjectCheck()
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

    public void ChangeWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null)
        {
            DropWeapon();

            currentWeapon = newWeapon;
            string temp = currentWeapon.transform.name;
            currentWeapon = Instantiate(currentWeapon, transform.position, transform.rotation);
            currentWeapon.transform.parent = gameObject.transform;
            currentWeapon.transform.name = temp;
            //currentWeapon.GetComponent<WeaponControl>().enabled = true;
            EnableScript(temp);
            currentWeaponName = currentWeapon.transform.name;

            if (transform.parent != null)
            {
                if (rootParent.tag == "Player")
                {
                    rootParent.GetComponent<PlayerControl>().assignWeapon(currentWeapon);
                }
                else //If enemy.
                {
                    rootParent.GetComponent<Enemy>().assignWeapon(currentWeapon);
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

            if (rootParent != null)
            {
                if (rootParent.tag == "Player")
                {
                    rootParent.GetComponent<PlayerControl>().assignWeapon(currentWeapon);
                }
                else //If enemy.
                {
                    rootParent.GetComponent<Enemy>().assignWeapon(currentWeapon);
                }
            }
        }
    }

    public void InitiateWeapon()
    {
        if (currentWeapon != null)
        {
            string temp = currentWeapon.transform.name;
            currentWeapon = Instantiate(currentWeapon, transform.position, transform.rotation);
            currentWeapon.transform.parent = gameObject.transform;
            currentWeapon.transform.name = temp;
            currentWeaponName = currentWeapon.transform.name;

            if (rootParent != null)
            {
                if (rootParent.tag == "Player")
                {
                    rootParent.GetComponent<PlayerControl>().assignWeapon(currentWeapon);
                }
                else //If enemy.
                {
                    rootParent.GetComponent<Enemy>().assignWeapon(currentWeapon);
                }
            }
        }
    }

    public void DropWeapon()
    {
        if (currentWeapon != null)
        {
            GameObject weaponDropped = Instantiate(weaponDrop, transform.position, Quaternion.Euler(0, 0, 0));
            weaponDropped.GetComponent<WeaponDrop>().SetWeapon(currentWeapon);
            Destroy(currentWeapon);
        }
    }

    private void EnableScript(string objectName)
    {
        if (objectName == "AK47")
        {
            currentWeapon.GetComponent<WP_AK47>().enabled = true;
        }
        else if (objectName == "GLOCK")
        {
            currentWeapon.GetComponent<WP_GLOCK>().enabled = true;
        }
    }
}
