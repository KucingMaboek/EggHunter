using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDrop : MonoBehaviour
{
    public float rotateSpeed;
    public GameObject weapon;
    public GameObject weaponAttached;
    private Text changeWeaponPopUp;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    public float amplitude;
    public float frequency;

    public string tempName;
    public int tempAmmo;
    // Start is called before the first frame update
    void Start()
    {
        changeWeaponPopUp = GameObject.Find("ChangeWeaponText").GetComponent<Text>();
        //changeWeaponPopUp = GetComponent<Text>();
        changeWeaponPopUp.enabled = false;
        posOffset = transform.position;
        weaponAttached = Instantiate(weapon, transform.position, transform.rotation);
        weaponAttached.transform.name = weapon.transform.name;
        weaponAttached.transform.parent = gameObject.transform;
        tempName = weaponAttached.transform.name;
        tempAmmo = weaponAttached.transform.GetComponent<WeaponControl>().stockAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            other.GetComponent<PlayerControl>().setWeaponDrop(gameObject.GetComponent<WeaponDrop>());
            if (tempName == other.transform.Find("WeaponSlot").GetComponent<WeaponSlot>().currentWeaponName)
            {
                GameObject playerWeapon = GameObject.Find("Player/WeaponSlot/" + tempName);
                playerWeapon.GetComponent<WeaponControl>().sumAmmo(tempAmmo);
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

    public void setWeapon(GameObject weapon)
    {
        this.weapon = weapon;
    }

    public void destroyWeaponDrop()
    {
        Destroy(gameObject);
    }

}
