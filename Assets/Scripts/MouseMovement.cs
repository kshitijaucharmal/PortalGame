using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovment : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRoatation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRoatation -= mouseY;
        xRoatation = Mathf.Clamp(xRoatation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRoatation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}