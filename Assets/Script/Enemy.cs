using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject weaponDrop;
    private GameObject currentWeapon;
    public GameObject weaponSlot;
    public HealthBar healthBar;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.angularDrag = 100f;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        onDeath();
    }

    public void dealDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }

    public void onDeath()
    {
        if (currentHealth <= 0)
        {
            GameObject weaponDropped = Instantiate(weaponDrop, transform.position, transform.rotation);
            weaponDropped.GetComponent<WeaponDrop>().SetWeapon(currentWeapon);
            Destroy(gameObject);
        }
    }

    public void assignWeapon(GameObject weapon)
    {
        currentWeapon = weapon;
    }
}
