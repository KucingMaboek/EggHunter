using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTiles : MonoBehaviour
{
    public float directionY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * directionY * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        directionY = -directionY;
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = null;
        }
    }
}