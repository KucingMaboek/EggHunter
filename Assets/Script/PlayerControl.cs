using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jump = 3f;
    [SerializeField] private float jumpRaycastDistance = 0.5f;
    public GameObject currentWeapon;
    private GameObject weaponSlot;
    public WeaponDrop weaponDrop;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.angularDrag = 100f;

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        isShoot();
        isReload();
        isSwitchingWeapon();
    }

    private void FixedUpdate()
    {
        Move();

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(0, jump, 0, ForceMode.Impulse);
            }
        }
    }

    private void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalAxis, 0, verticalAxis) * speed * Time.deltaTime;
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);
        rb.MovePosition(newPosition);
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * jumpRaycastDistance, Color.blue);
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);
    }

    private void isShoot()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("player shoot");
            currentWeapon.GetComponent<WeaponControl>().Shoot();
        }
    }

    private void isReload()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            currentWeapon.GetComponent<WeaponControl>().Reload();
        }
    }

    private void isSwitchingWeapon()
    {
        if (weaponDrop != null)
        {
            if (Input.GetKeyUp(KeyCode.G))
            {
                weaponSlot.GetComponent<WeaponSlot>().changeWeapon(weaponDrop.weaponAttached);
                weaponDrop.destroyWeaponDrop();
            }
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
        this.weaponDrop = weaponDrop;
    }

    public void clearWeaponDrop()
    {
        weaponDrop = null;
    }
}
