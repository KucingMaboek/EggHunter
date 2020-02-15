using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public GameObject weaponDrop;
    private GameObject currentWeapon;
    public GameObject weaponSlot;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.angularDrag = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            onDeath();
        }
    }

    public void dealDamage(float damage)
    {
        health -= damage;
    }

    public void onDeath()
    {

        //Vector3 tempRotation = Quaternion.EulerAngles(transform.rotation);
        GameObject weaponDropped = Instantiate(weaponDrop, transform.position, transform.rotation);
        weaponDropped.GetComponent<WeaponDrop>().setWeapon(currentWeapon);
        Destroy(gameObject);
    }

    public void assignWeapon(GameObject weapon)
    {
        currentWeapon = weapon;
    }
}
