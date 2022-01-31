using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightCameraRig : MonoBehaviour
{
    public float flySpeed = 10;

    
    public float mouseSensitivityX = 1f;
    public float mouseSensitivityY = 1f;

    private float pitch = 0, yaw = 0;
    
    // Update is called once per frame
    void Update()
    {
        // Update Position
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float d = Input.GetAxis("Jump");

        Vector3 direction = transform.forward * v + transform.right * h + transform.up * d;
        if (direction.magnitude > 1) direction.Normalize();

        transform.position += direction * Time.deltaTime * flySpeed;

        // Update Rotation

        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivityX; // Yaw (y)
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivityY; // Pitch (x)

            pitch = Mathf.Clamp(pitch, -89, 89);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }

        flySpeed = (Input.GetKey("left shift")) ? 30 : 10;
            
        

    }
}
