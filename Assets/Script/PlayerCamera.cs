using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity = 100f;
    private float xRotation = 0f;
    private GameObject player;

    public FireButton fireButton;
    public TouchField touchField;

    private float MouseX;
    private float MouseY;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireButton.IsFiring())
        {
            MouseX = fireButton.Horizontal() * cameraSensitivity * Time.deltaTime;
            MouseY = fireButton.Vertical() * cameraSensitivity * Time.deltaTime;
        }
        else
        {
            MouseX = touchField.Horizontal() * cameraSensitivity * Time.deltaTime;
            MouseY = touchField.Vertical() * cameraSensitivity * Time.deltaTime;
        }


        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.transform.Rotate(Vector3.up * MouseX);

    }
}
