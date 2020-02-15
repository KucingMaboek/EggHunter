using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTiles : MonoBehaviour
{
    public float directionX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * directionX * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        directionX = -directionX;
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
