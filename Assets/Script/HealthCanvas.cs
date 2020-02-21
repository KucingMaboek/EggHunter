using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCanvas : MonoBehaviour
{
    public Transform tpsCam;

    private void LateUpdate()
    {
        transform.LookAt(transform.position+tpsCam.forward);
    }
}
