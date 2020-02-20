using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDrop : MonoBehaviour
{
    private float rotateSpeed = 40f;
    public GameObject weapon;
    private GameObject weaponAttached;
    private Text changeWeaponPopUp;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    private float amplitude = 0.5f;
    private float frequency = 0.5f;

    public string tempName;
    public int tempAmmo;
    // Start is called before the first frame update
    void Start()
    {

        posOffset = transform.position;
        weaponAttached = Instantiate(weapon, transform.position, transform.rotation);
        weaponAttached.transform.name = weapon.transform.name;
        weaponAttached.transform.parent = gameObject.transform;
        tempName = weaponAttached.transform.name;
        //tempAmmo = weaponAttached.transform.GetComponent<WeaponControl>().stockAmmo;
        tempAmmo = weaponAttached.transform.GetComponent<IWeaponControl>().GetStockAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        Floating();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            changeWeaponPopUp = other.transform.Find("MainCanvas").Find("ChangeWeaponText").GetComponent<Text>();
            other.GetComponent<PlayerControl>().setWeaponDrop(gameObject.GetComponent<WeaponDrop>());
            if (tempName == other.transform.Find("WeaponSlot").GetComponent<WeaponSlot>().currentWeaponName)
            {
                GameObject playerWeapon = other.transform.Find("WeaponSlot").GetComponent<WeaponSlot>().currentWeapon;
                //playerWeapon.GetComponent<WeaponControl>().sumAmmo(tempAmmo);
                playerWeapon.GetComponent<IWeaponControl>().AddAmmo(tempAmmo);
                Destroy(gameObject);
            } else
            {
                changeWeaponPopUp.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player")
        {
            other.GetComponent<PlayerControl>().clearWeaponDrop();
            changeWeaponPopUp.enabled = false;
        }
    }

    public void SetWeapon(GameObject weapon)
    {
        this.weapon = weapon;
    }

    public void DestroyWeaponDrop()
    {
        Destroy(gameObject);
        changeWeaponPopUp.enabled = false;
    }

    private void Floating()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    public GameObject GetWeaponAttached()
    {
        return weaponAttached;
    }
}
