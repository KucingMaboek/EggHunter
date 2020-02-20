using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public GameObject hitPosition;
    public Transform noTarget;
    public float damage; 
    public RaycastHit raycastHit;
    private Vector3 target;
    private Vector3 current;

    // Use this for initialization
    void Start()
    {
        raycastHit = transform.parent.GetComponent<WeaponSlot>().hit;
        target = raycastHit.point;
        current = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObjectCheck())
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, noTarget.transform.position, speed * Time.deltaTime);
        }
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.rigidbody != null)
        {
            collision.rigidbody.AddForce(-raycastHit.normal * gameObject.GetComponent<Rigidbody>().mass * speed );
        }
        if (collision.transform.GetComponent<Enemy>() != null)
        {
            collision.transform.GetComponent<Enemy>().dealDamage(damage);
        }
        GameObject hitEffect = Instantiate(hitPosition, transform.position, transform.rotation);
        Destroy(hitEffect, 0.5f);
        Destroy(gameObject);
    }

    private bool targetObjectCheck()
    {
        try
        {
            string checkString = raycastHit.transform.name;
            return true;
        }
        catch (System.Exception e)
        {
            string exception = e.Message;
            return false;
        }

    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setNoTarget(Transform noTarget)
    {
        this.noTarget = noTarget;
    }
}
