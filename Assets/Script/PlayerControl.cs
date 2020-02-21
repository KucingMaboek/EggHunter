using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jump = 3f;
    private float jumpRaycastDistance = 1.1f;
    private GameObject currentWeapon;
    private GameObject weaponSlot;
    private WeaponDrop weaponOnFloor;
    public GameObject weaponDrop;
    public HealthBar healthBar;
    private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public JoyStick playerController;

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
        //isSwitchingWeapon();
        onDeath();
        try
        {
            currentWeapon.GetComponent<IWeaponControl>().ShowAmmoOnUI();
        }
        catch (System.Exception e)
        {
            string exception = e.Message;
            //Debug.Log(e.Message);
        }
    }

    private void FixedUpdate()
    {
        Move();

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
            //Destroy(gameObject);
        }
    }

    public void Jump()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (isGrounded())
        //    {
        //        rb.AddForce(0, jump, 0, ForceMode.Impulse);
        //    }
        //}

        if (isGrounded())
        {
            rb.AddForce(0, jump, 0, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        //float horizontalAxis = Input.GetAxis("Horizontal");
        //float verticalAxis = Input.GetAxis("Vertical");

        float horizontalAxis = playerController.Horizontal();
        float verticalAxis = playerController.Vertical();
        Vector3 movement = new Vector3(horizontalAxis, 0, verticalAxis) * speed * Time.deltaTime;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);
        rb.MovePosition(newPosition);
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * jumpRaycastDistance, Color.blue);
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);
    }

    public void isShoot()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    currentWeapon.GetComponent<WeaponControl>().Shoot();
        //}

        //currentWeapon.GetComponent<WeaponControl>().Shoot();
        currentWeapon.GetComponent<IWeaponControl>().TriggerOnHold();
    }

    public void isReload()
    {
        //if (Input.GetKeyUp(KeyCode.R))
        //{
        //    currentWeapon.GetComponent<WeaponControl>().Reload();
        //}

        //currentWeapon.GetComponent<WeaponControl>().Reload();
        currentWeapon.GetComponent<IWeaponControl>().Reload();
    }

    public void isSwitchingWeapon()
    {
        if (weaponOnFloor != null)
        {
            //if (Input.GetKeyUp(KeyCode.G))
            //{
            weaponSlot.GetComponent<WeaponSlot>().ChangeWeapon(weaponOnFloor.GetWeaponAttached());
            weaponOnFloor.DestroyWeaponDrop();
            //}
        }
    }

    public void assignWeapon(GameObject weapon)
    {
        currentWeapon = weapon;
    }

    public void setWeaponSlot(GameObject weaponSlot)
    {
        this.weaponSlot = weaponSlot;
    }

    public void setWeaponDrop(WeaponDrop weaponDrop)
    {
        this.weaponOnFloor = weaponDrop;
    }

    public void clearWeaponDrop()
    {
        weaponOnFloor = null;
    }
}
