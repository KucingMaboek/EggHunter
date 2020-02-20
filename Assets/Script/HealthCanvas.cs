using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCanvas : MonoBehaviour
{
    public Transform fpsCam;

    private void LateUpdate()
    {
        transform.LookAt(transform.position+fpsCam.forward);
    }
}
